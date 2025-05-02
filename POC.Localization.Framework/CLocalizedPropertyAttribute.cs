using System.Reflection;

namespace POC.Localization.Framework;

[AttributeUsage(AttributeTargets.Property)]
public class CLocalizedPropertyAttribute : Attribute
{
    #region Constructor

    public CLocalizedPropertyAttribute(Type _ResourceType, string _strResourcePropertyName)
    {
        this.ResourceKey = CResourceHelper.GetResourceKeyFromGetter(_ResourceType.Assembly.Location, _ResourceType!.FullName, "get_" + _strResourcePropertyName);
    }

    #endregion

    #region Description

    public string? ResourceKey { get; }

    #endregion
}