using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private StringEventChannel loadSceneEventChannel;
    [SerializeField] private SceneTransitionSpawnPoints sceneTransitionSpawnPoints;
    private void OnTriggerEnter(Collider other)
    {
        var fromScene = SceneManager.GetActiveScene().name;
        var sceneTransition = fromScene + "_" + sceneToLoad;
        var newSpawnPoint = sceneTransitionSpawnPoints.transitionSpawnPoints[sceneTransition];
        SaveData.Current.lastCheckPointPositionData = newSpawnPoint;
        SaveData.Current.lastLoadedScene = sceneToLoad;
        SaveSystemManager.Instance.Save();
        loadSceneEventChannel.Raise(sceneToLoad);
        Debug.Log("switching scene");
    }
}

[Serializable]
public struct SceneTransition
{
    public string fromScene;
    public string toScene;
}