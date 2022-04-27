using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinEquipmentTesting : MonoBehaviour
{
    // Start is called before the first frame update

    public Mesh mesh;
    public Transform renderer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("create test")]
    public void Test()
    {
        renderer.GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh;
        renderer.GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh;
        
    }
}
