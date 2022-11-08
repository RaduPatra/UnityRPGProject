using UnityEngine;

public class TransformToTargetRayProvider : MonoBehaviour, IRayProvider
{
    [SerializeField] private float yRayOffset = 1.2f;
    
    private EnemyAI enemy;
    private void Awake()
    {
        enemy = GetComponent<EnemyAI>();//make interface for curr target instead of enemyAI?
    }

    public Ray CreateRay()
    {
        var targetPos = enemy.currentTargetInfo.targetTransform.position + new Vector3(0, yRayOffset, 0);
        var myPosition = transform.position;
        var dirToTarget = (targetPos - myPosition).normalized;
        return new Ray(myPosition, dirToTarget);
    }
}