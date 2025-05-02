using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.Globalization;

namespace POC.Localization.WPF.Framework;

public class CStringLocalizerService : IStringLocalizer, INotifyPropertyChanged
{
    #region Singleton

    private static CStringLocalizerService _instance = null!;
    public static CStringLocalizerService Instance
    {
        get
        {
            if (_instance == null)
                _instance = new CStringLocalizerService();
            return _instance;
        }
    }

    #endregion

    #region Members

    private IDictionary<string, IDictionary<string, string>> m_TranslationCache = new Dictionary<string, IDictionary<string, string>>();

    #endregion

    #region Culture

    private string m_strSelectedCulture = CultureInfo.CurrentCulture.Name;
    public string SelectedCulture
    {
        get => m_strSelectedCulture;
        set
        {
            if (m_strSelectedCulture == value)
                return;
            m_strSelectedCulture = value;
            OnPropertyChanged(nameof(SelectedCulture));
        }
    }
    public event EventHandler? CultureChanged;

    private void RaisCultureChanged()
    {
        CultureChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateCulture(string _strCulture)
    {
        SelectedCulture = _strCulture;
        RaisCultureChanged();
    }

    #endregion

    #region Load translations
    /// For the sake of the POC, this method is used to retrieve the translations from the resource file directly, but in a real world scenario, this should be done using a service that retrieves the translations from an online database.
    /// </summary>
    public void AddTranslation(string _strCulture, string _strTranslationKey, string _strTranslationText)
    {
        if(!m_TranslationCache.ContainsKey(_strCulture))
            m_TranslationCache.Add(_strCulture, new Dictionary<string, string>());
        IDictionary<string, string> cultureDictionary = m_TranslationCache[_strCulture];
        if(!cultureDictionary.ContainsKey(_strTranslationKey))
            cultureDictionary.Add(_strTranslationKey, _strTranslationText);
        else
            cultureDictionary[_strTranslationKey] = _strTranslationText;
    } 

    #endregion

    #region Get translation string

    public string Translate(string _strCulture, string _TranslationKey)
    {
        // translate here using local cache
        if(m_TranslationCache.ContainsKey(_strCulture) && m_TranslationCache[_strCulture].ContainsKey(_TranslationKey))
            return m_TranslationCache[_strCulture][_TranslationKey];
        return _TranslationKey;
    }

    #endregion

    #region IStringLocalizer Members

    // TODO: all interface members not implemented because not needed in the POC
    public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

    public LocalizedString this[string name] => throw new NotImplementedException();

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    #endregion
}