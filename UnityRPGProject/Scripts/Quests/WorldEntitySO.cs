using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Entity", menuName = "WorldEntity", order = 6)]
public class WorldEntitySO : ScriptableObject, IDatabaseItem
{
    [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
}