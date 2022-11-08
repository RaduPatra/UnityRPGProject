using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFadeTransition : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float endTransitionDuration = 1.75f;
    [SerializeField] private float startTransitionDuration = 1.75f;
    [SerializeField] private StringEventChannel loadSceneEventChannel;
    [SerializeField] private VoidEventChannel reloadSceneEventChannel;
    // [SerializeField] private StringEventChannel beforeSceneLoadEventChannel;
    private static readonly int End = Animator.StringToHash("End");
    private static readonly int StartFade = Animator.StringToHash("Start");

    private void Awake()
    {
        loadSceneEventChannel.Listeners += LoadScene;
        reloadSceneEventChannel.Listeners += ReloadScene;
        animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        loadSceneEventChannel.Listeners -= LoadScene;
        reloadSceneEventChannel.Listeners -= ReloadScene;
    }

    private void Start()
    {
        StartCoroutine(StartFadeOut(startTransitionDuration));
    }

    private IEnumerator StartFadeOut(float transitionDuration)
    {
        yield return new WaitForSecondsRealtime(transitionDuration);
        animator.SetTrigger(StartFade);
    }

    private void LoadScene(string sceneName)
    {
        animator.SetTrigger(End);
        StartCoroutine(LoadSceneCo(sceneName));
    }

    [ContextMenu("Reload Scene Test")]
    public void ReloadScene()
    {
        animator.SetTrigger(End);
        StartCoroutine(ReloadSceneCo());
    }

    private IEnumerator LoadSceneCo(string sceneName)
    {
        yield return new WaitForSecondsRealtime(endTransitionDuration);
        // beforeSceneLoadEventChannel.Raise(sceneName);
        // SaveData.Current.lastLoadedScene = sceneName;
        // SaveSystemManager.Instance.Save();
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator ReloadSceneCo()
    {
        yield return new WaitForSecondsRealtime(endTransitionDuration);
        // SaveData.Current.lastLoadedScene = SceneManager.GetActiveScene().name;
        // SaveSystemManager.Instance.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}