using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private StringEventChannel loadSceneEventChannel;

    private bool isGamePaused;

    private void Awake()
    {
        inputManager.pauseGameAction += PauseGame;
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        inputManager.pauseGameAction -= PauseGame;
    }

    private void PauseGame()
    {
        if (isGamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inputManager.mouseLookReference.action.Enable();
    }

    public void Exit()
    {
        loadSceneEventChannel.Raise("MainMenu");
    }


    private void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        inputManager.mouseLookReference.action.Disable();

        //disable actions?
    }
}