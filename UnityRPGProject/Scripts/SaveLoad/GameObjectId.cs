using System;
using System.Data;
using UnityEngine;


public class GameObjectId : MonoBehaviour
{
    public string Id = Guid.NewGuid().ToString();

  
    private void Awake()
    {
    }

    private void Start()
    {
    }
    
    [ContextMenu("Generate New ID")]
    public void GenerateNewId()
    {
        Id = Guid.NewGuid().ToString();
    }
}