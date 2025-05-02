namespace POC.Localization.Framework;
public class ChatMessageViewModel
{
    public ChatMessageViewModel(string _strTranslationKey)
    {
        TranslationKey = _strTranslationKey;
    }

    public string TranslationKey { get; }
}
