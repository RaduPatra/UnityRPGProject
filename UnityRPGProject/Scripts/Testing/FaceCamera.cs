using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        var dirToCamera = (cam.transform.position - transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(dirToCamera);
        transform.rotation = targetRotation;

    }
}