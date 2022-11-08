using System;
using ExtensionMethods;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class Checkpoint : MonoBehaviour
{
    public Transform spawnTransform;

    private void Start()
    {
    }

    private void Awake()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        SaveData.Current.lastCheckPointPositionData = spawnTransform.position;
        SaveSystemManager.Instance.Save();
    }
}

[Serializable]
public class CheckPointSaveData
{
    public bool isFirstInteraction;
}