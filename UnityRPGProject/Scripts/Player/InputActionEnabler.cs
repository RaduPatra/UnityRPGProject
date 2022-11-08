using UnityEngine;

//enables action for the input SO.
//Used for when the scene reloads since OnEnable/OnDisable for ScriptableObjects
//doesn't get called all the time on reload
public class InputActionEnabler : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    private void Awake()
    {
        inputManager.ToggleAllActions(true);
    }
}