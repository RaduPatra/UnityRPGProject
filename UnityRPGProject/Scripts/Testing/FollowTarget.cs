using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Camera cam;

    private void Awake()
    {
        if (target == null)
            target = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        var transform1 = transform;
        transform1.position = target.transform.position;
        // var dirToCamera = (cam.transform.position - transform.position).normalized;
        var dirFromCamera = (transform1.position - cam.transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(dirFromCamera);
        transform.rotation = targetRotation;
    }
}