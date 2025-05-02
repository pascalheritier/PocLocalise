using POC.Localization.Framework;
using POC.Localization.WPF.Framework;
using System.Globalization;
using System.Windows;

namespace POC.Localization.WPF.App;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static string TestCulture1 = CultureInfo.CurrentCulture.Name;
    public static string TestCulture2 = "en-US";

    public MainWindow()
    {
        LoadTranslations(); // this should be done elswehere, e.g. in a bootstrapper
        InitializeComponent();
    }

    private void LoadTranslations()
    {
        // real context: get translation keys and texts from online database
        // poc context: use the resx for the sake of demonstration, append culture to text to visualize effect on translation

        // Default culture
        CStringLocalizerService.Instance.AddTranslation(
            TestCulture1,
            CResourceHelper.GetResourceKeyFromPropertyName(nameof(POC_Localization_WPF_App.UI_Text)),
            POC_Localization_WPF_App.UI_Text + " " + TestCulture1);

        CStringLocalizerService.Instance.AddTranslation(
            TestCulture1,
            CResourceHelper.GetResourceKeyFromPropertyName(nameof(POC_Localization_WPF_App.UI_Text2)),
            POC_Localization_WPF_App.UI_Text2 + " " + TestCulture1);

        CStringLocalizerService.Instance.AddTranslation(
            TestCulture1,
            CResourceHelper.GetResourceKeyFromPropertyName(nameof(POC_Localization_WPF_App.UI_Text3)),
            POC_Localization_WPF_App.UI_Text3 + " " + TestCulture1);

        // Test culture
        CStringLocalizerService.Instance.AddTranslation(
            TestCulture2,
            CResourceHelper.GetResourceKeyFromPropertyName(nameof(POC_Localization_WPF_App.UI_Text)),
            POC_Localization_WPF_App.UI_Text + " " + TestCulture2);

        CStringLocalizerService.Instance.AddTranslation(
            TestCulture2,
            CResourceHelper.GetResourceKeyFromPropertyName(nameof(POC_Localization_WPF_App.UI_Text2)),
            POC_Localization_WPF_App.UI_Text2 + " " + TestCulture2);

        CStringLocalizerService.Instance.AddTranslation(
            TestCulture2,
            CResourceHelper.GetResourceKeyFromPropertyName(nameof(POC_Localization_WPF_App.UI_Text3)),
            POC_Localization_WPF_App.UI_Text3 + " " + TestCulture2);
    }

    private void TranslateToCulture1(object sender, RoutedEventArgs e)
    {
        CStringLocalizerService.Instance.UpdateCulture(TestCulture1);
    }

    private void TranslateToCulture2(object sender, RoutedEventArgs e)
    {
        CStringLocalizerService.Instance.UpdateCulture(TestCulture2);
    }
}