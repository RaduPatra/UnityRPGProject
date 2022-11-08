using System;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

public class RaycastTester : MonoBehaviour
{
    public LayerMask layerMask1;
    public LayerMask layerMask2;
    public float rayDistance = 20f;
    
    
    private void Awake()
    {

    }

    public Transform hitTransform;
    public bool raycastResult;

    public void Update()
    {
        RaycastHit hit;
        raycastResult = Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, layerMask1);


        hitTransform = hit.transform;
    }


    private void OnDrawGizmos()
    {
        var position = transform.position;
        var forward = transform.forward;
        Gizmos.DrawRay(position, forward * rayDistance);
    }

    private void LateUpdate()
    {
    }
}