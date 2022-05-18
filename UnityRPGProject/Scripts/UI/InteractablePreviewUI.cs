using System;
using TMPro;
using UnityEngine;


public interface IInitializable
{
    public void Initialize();
}

public class InteractablePreviewUI : MonoBehaviour, IInitializable
{
    [SerializeField] private TextMeshProUGUI previewText;
    
    [SerializeField] private StringEventChannel interactPreviewEventChannel;
    [SerializeField] private VoidEventChannel interactExitPreviewEventChannel;

    public void Initialize()
    {
        interactPreviewEventChannel.Listeners += ShowPreview;
        interactExitPreviewEventChannel.Listeners += HidePreview;
    }

    private void ShowPreview(string preview)
    {
        gameObject.SetActive(true);
        previewText.text = preview;
    }

    private void HidePreview()
    {
        previewText.text = "";
        gameObject.SetActive(false);
    }
}