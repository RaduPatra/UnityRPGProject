using TMPro;
using UnityEngine;

public class DialogueOptionUI : MonoBehaviour
{
    private TextMeshProUGUI optionText;

    private void Awake()
    {
        optionText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        optionText.text = text;
    }
}