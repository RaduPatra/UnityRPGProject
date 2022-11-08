using System;
using UnityEngine;


public class TestMono : MonoBehaviour
{
    public Rigidbody rb;
    public KeyCode moveKey;
    public float moveSpeed;
    public int moveDir = 1;
    


    [ContextMenu("TestType")]
    public void Test()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        if (Input.GetKey(moveKey))
        {
            rb.AddForce(0, 0, moveSpeed * moveDir * Time.deltaTime);
        }
    }
}