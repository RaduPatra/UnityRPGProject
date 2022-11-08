using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class AIFollower : MonoBehaviour, IWorldEntity
{
    [field: SerializeField] public WorldEntitySO WorldEntity { get; set; }
    public NavMeshAgent navMeshAgent;
    public Transform currentTarget;
    private CharacterAnimator characterAnimator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterAnimator = GetComponent<CharacterAnimator>();
        SaveSystem.OnBeforeLoad += LoadFollower;
        SaveSystem.OnInitSaveData += InitData;
    }

    private void InitData()
    {
        SaveData.Current.sceneData.followersData[GetComponent<GameObjectId>().Id] =
            new FollowerData { followerPosition = transform.position, followerRotation = transform.rotation};
    }

    private void OnDestroy()
    {
        SaveSystem.OnBeforeLoad -= LoadFollower;
    }

    private void LoadFollower(SaveData saveData)
    {
        Destroy(gameObject);

        /*if (saveData.followerId == GetComponent<GameObjectId>().Id)
        {
            Destroy(gameObject);
        }
        else
        {
            var followerPos = saveData.sceneData.followersData[GetComponent<GameObjectId>().Id];
            navMeshAgent.Warp(followerPos.followerPosition);
        }*/
        
    }

    public void FollowTarget(Transform target)
    {
        SaveData.Current.sceneData.followersData.Remove(GetComponent<GameObjectId>().Id);
        currentTarget = target;
    }

    public void StopFollow()
    {
        currentTarget = null;
    }

    private void Update()
    {
        // if (currentTarget == null) return;
        if (characterAnimator.IsInteracting)
        {
            navMeshAgent.velocity = Vector3.zero;
        }

        var animVal = (navMeshAgent.velocity != Vector3.zero) ? 1 : 0;
        characterAnimator.animator.SetFloat(CharacterAnimator.Vertical, animVal, .2f, Time.deltaTime);

        if (currentTarget != null)
        {
            navMeshAgent.SetDestination(currentTarget.position);
        }
    }

    public bool LookToTransform(Transform rotateTarget)
    {
        var targetPos = rotateTarget.position;
        var direction = (targetPos - transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(direction);
        targetRotation.x = 0;
        targetRotation.z = 0;
        var rotation = transform.rotation;
        rotation =
            Quaternion.Slerp(rotation, targetRotation, 5f * Time.deltaTime);
        transform.rotation = rotation;

        // Debug.Log("rot : : " + rotation);
        // Debug.Log("targRot : : " + targetRotation);
        return Math.Abs(Math.Floor(rotation.eulerAngles.y) - Math.Floor(targetRotation.eulerAngles.y)) < 1;

    }
}