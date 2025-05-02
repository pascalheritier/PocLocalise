
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using System.Linq;

/// <summary>
/// This class is used solely to automatically generate a static property holding the name of the property holding the key/name in the resx file, since it is
/// not possible to get a dynamic nameof in XAML.
/// </summary>
[Generator]
public class ResxKeyWrapperGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context) { }

    public void Execute(GeneratorExecutionContext context)
    {
        var sb = new StringBuilder(@"
using POC.Localization.WPF.Framework;

namespace POC.Localization.WPF.App
{
    public class CResourceProvider : IResourceProvider
    {
        public string ResourcePropertyName { get; }

        private CResourceProvider(string _strResourcePropertyName)
        {
            ResourcePropertyName = _strResourcePropertyName;
        }
");

        var resourceClass = context.Compilation.SyntaxTrees
            .SelectMany(tree => tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>())
            .FirstOrDefault(c => c.Identifier.Text == "POC_Localization_WPF_App");

        if (resourceClass != null)
        {
            var model = context.Compilation.GetSemanticModel(resourceClass.SyntaxTree);
            var symbol = model.GetDeclaredSymbol(resourceClass) as INamedTypeSymbol;

            foreach (var prop in symbol.GetMembers().OfType<IPropertySymbol>().Where(p => p.Type.Name == "String"))
            {
                var propName = prop.Name;
                sb.AppendLine($@"        public static CResourceProvider {propName} = new(""{propName}"");");
            }
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");
        context.AddSource("CResourceProvider.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
    }
}
