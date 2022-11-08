using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DeadUI : MonoBehaviour
{
    [SerializeField] private VoidEventChannel deathEventChannel;
    [SerializeField] private float showAfterSeconds = 1f;
    [SerializeField] private GameObject deathPanel;
    private TextMeshProUGUI deadText;

    private void Awake()
    {
        deathEventChannel.Listeners += ShowDeadText;
        // deadText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnDestroy()
    {
        deathEventChannel.Listeners -= ShowDeadText;

    }

    private void ShowDeadText()
    {
        StartCoroutine(ShowTextCo());
    }

    private IEnumerator ShowTextCo()
    {
        yield return new WaitForSecondsRealtime(showAfterSeconds);
        // deadText.enabled = true;
        deathPanel.SetActive(true);
    }
}