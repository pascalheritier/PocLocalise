using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace POC.Localization.WPF.Framework;
public class CLocaliseChatMessageExtension : MarkupExtension
{
    public BindingBase TranslationKeyBinding { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (TranslationKeyBinding is null)
            return "[null key]";

        var multiBinding = new MultiBinding { Mode = BindingMode.OneWay };
        multiBinding.Bindings.Add(TranslationKeyBinding);
        multiBinding.Converter = new LocaliseInternalConverter();

        return multiBinding.ProvideValue(serviceProvider);
    }

    private class LocaliseInternalConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string translationKey = values[0]?.ToString();
            return CStringLocalizerService.Instance.Translate(CStringLocalizerService.Instance.SelectedCulture, translationKey);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}