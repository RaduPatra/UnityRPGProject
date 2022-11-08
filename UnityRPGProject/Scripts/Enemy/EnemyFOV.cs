using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyFOV : MonoBehaviour
{
    public float fovRadius;
    public float fovAngle;
    public float fovPitch = 7f;
    public float sphereCastRadius = .2f;
    public LayerMask targetColliders;
    public LayerMask nonObstacleColliders;
    public List<Transform> targetsInFOV = new List<Transform>();

    private Vector3 directionFromEnemyToTarget;
    private Vector3 directionFromEnemyToTargetTest;


    public float targetFoundCooldown = 1f;
    public float currentTargetFoundCooldown;
    
    public float yAngleTest = 10f;
    public float angleToTargetDebug;
    public float pitchDebug;

    private void Update()
    {
        var position = transform.position;
        var hitColliders = Physics.OverlapSphere(position, fovRadius, targetColliders);
        var enemyForward = transform.forward;
        currentTargetFoundCooldown -= Time.deltaTime;

        if (currentTargetFoundCooldown < 0)
        {
            targetsInFOV.Clear();
            currentTargetFoundCooldown = targetFoundCooldown;
        }

        foreach (var hitCollider in hitColliders)
        {
            directionFromEnemyToTarget = (hitCollider.transform.position - position);
            directionFromEnemyToTarget.y += 1;
            directionFromEnemyToTarget = Vector3.Normalize(directionFromEnemyToTarget);
            var pitch = Mathf.Asin(directionFromEnemyToTarget.y) * Mathf.Rad2Deg;
            var angleToTarget = Vector3.Angle(enemyForward, directionFromEnemyToTarget);
            angleToTargetDebug = angleToTarget;
            pitchDebug = pitch;
            if (angleToTarget < fovAngle / 2 && Mathf.Abs(pitch) < fovPitch)
            {
                RaycastHit hit;
                if (Physics.Raycast(position, directionFromEnemyToTarget, out hit, fovRadius))
                {
                    if (IsTargetValid(hit.collider.transform))
                    {
                        targetsInFOV.Add(hitCollider.transform);
                    }
                }
            }
        }
    }

    public bool IsTargetValid(Transform target)
    {
        return Utils.IsInLayerMask(target.gameObject.layer, nonObstacleColliders);
    }

    public bool IsTargetInFov(Transform target)
    {
        return targetsInFOV.Contains(target);
    }


    [SerializeField, ReadOnly] private Transform closestTarget;

    public Transform GetClosestTarget()
    {
        closestTarget = Utils.GetClosestTransformInList(transform, targetsInFOV);
        return closestTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var position = transform.position;
        var forward = transform.forward;
        Gizmos.DrawWireSphere(position, fovRadius);


        Gizmos.color = Color.green;
        float totalFOV = fovAngle;
        float rayRange = fovRadius;
        float halfFOV = totalFOV / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * forward;
        Vector3 rightRayDirection = rightRayRotation * forward;
        Gizmos.DrawRay(position, leftRayDirection * rayRange);
        Gizmos.DrawRay(position, rightRayDirection * rayRange);

        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(position, directionFromEnemyToTarget * fovRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(position, directionFromEnemyToTargetTest * fovRadius);
    }
}