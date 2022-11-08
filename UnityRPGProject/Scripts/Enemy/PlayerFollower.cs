using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private AIFollower followerPrefab;
    public AIFollower currentFollower;

    private void Awake()
    {
        SaveSystem.OnBeforeLoad += SpawnFollower;
        SaveSystem.OnLoad += LoadFollower;
    }

 
    private void SpawnFollower(SaveData saveData)
    {
        /*if (saveData.followerId != "")
        {
            InstantiateFollower(saveData);
        }*/
    }
    private void LoadFollower(SaveData saveData)
    {
        if (saveData.followerId != "")
        {
            InstantiateFollower(saveData);
        }
        
        if (currentFollower)
        {
            currentFollower.GetComponent<GameObjectId>().Id = saveData.followerId;
            SetFollower(currentFollower);
        }
    }

    private void OnDestroy()
    {
        SaveSystem.OnBeforeLoad -= SpawnFollower;
    }

    public void SetFollower(AIFollower follower)
    {
        follower.FollowTarget(transform);
        currentFollower = follower;
        SaveData.Current.followerId = follower.GetComponent<GameObjectId>().Id;
    }

    public void RemoveFollower()
    {
        currentFollower.StopFollow();
        currentFollower = null;
        SaveData.Current.followerId = "";
    }

    private void InstantiateFollower(SaveData saveData)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(saveData.lastCheckPointPositionData + Vector3.forward * 3f, out hit, 100f,
            NavMesh.AllAreas))
        {
            currentFollower = Instantiate(followerPrefab, hit.position, Quaternion.identity);
            currentFollower.GetComponent<DialogueSource>().LoadDefaultDialogue(saveData);
        }
    }
}