using System;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject continueGameButtonPrefab;
    public Transform continueGameParent;
    public StringEventChannel loadSceneEventChannel;

    public string firstSceneName = "Main";

    private void Awake()
    {
        Time.timeScale = 1;
        var data = SaveSystem.GetDataFromFile<SaveData>();
        if (!data.lastLoadedScene.IsNullOrWhitespace())
        {
            var btnGo = Instantiate(continueGameButtonPrefab, continueGameParent);
            var btn = btnGo.GetComponent<Button>();
            btn.onClick.AddListener(ContinueGame);
        }
    }

    public void PlayNewGame()
    {
        SaveSystem.ResetData();
        loadSceneEventChannel.Raise(firstSceneName);
    }

    public void ContinueGame()
    {
        var data = SaveSystem.GetDataFromFile<SaveData>();
        loadSceneEventChannel.Raise(data.lastLoadedScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}