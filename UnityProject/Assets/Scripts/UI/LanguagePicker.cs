using UnityEngine;
using UnityEngine.UI;

public class LanguagePicker : MonoBehaviour
{
    public Button EnglishBtn;
    public Button SpanishBtn;

    void Start()
    {
        if (EnglishBtn) EnglishBtn.onClick.AddListener(()=>Choose("en"));
        if (SpanishBtn) SpanishBtn.onClick.AddListener(()=>Choose("es"));
    }

    void Choose(string code)
    {
        LanguageManager.Code = code;
        // Hide the picker after selection
        gameObject.SetActive(false);
        // Notify systems
        EventBus.Broadcast("lang.changed", code);
    }
}
