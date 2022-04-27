using UnityEngine;

public class BackAndForthMover : MonoBehaviour
{
    Vector3 pointA = new Vector3(0, 0, 0);
    Vector3 pointB = new Vector3(1, 1, 1);

    public Transform transformA;
    public Transform transformB;
    void Update()
    {
        transform.position = Vector3.Lerp(transformA.position, transformB.position, Mathf.PingPong(Time.time, 1));
    }
}