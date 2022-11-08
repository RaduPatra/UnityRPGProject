using UnityEngine;
using UnityEngine.InputSystem;

public abstract class EnemyState : MonoBehaviour
{
    public abstract EnemyState Tick(EnemyAIOld enemy);
}