using UnityEngine;

public class CanvasAssigner : MonoBehaviour
{
    [SerializeField] private GameObjectEventChannel canvasGoEventChannel;
    private void Start()
    {
        if (canvasGoEventChannel != null)
            canvasGoEventChannel.Raise(gameObject);
    }
}