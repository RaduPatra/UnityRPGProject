using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "new SceneTransitionSpawnPoints", menuName = "SceneTransitionSpawnPoints", order = 1)]

public class SceneTransitionSpawnPoints : SerializedScriptableObject
{
    public Dictionary<string, Vector3> transitionSpawnPoints = new Dictionary<string, Vector3>();
}