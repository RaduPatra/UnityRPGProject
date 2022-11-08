using UnityEngine;

public class CameraRayProvider : MonoBehaviour, IRayProvider
{
    public Ray CreateRay()
    {
        var cam = Camera.main;
        var center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        return cam is { } ? cam.ScreenPointToRay(center) : new Ray(Vector3.zero, Vector3.zero);
    }
}