using UnityEngine;

public class FollowerLoader : MonoBehaviour
{
    public GameObject followerPrefab;
    private void Awake()
    {
        SaveSystem.OnBeforeLoad += Load;
    }
    private void OnDestroy()
    {
        SaveSystem.OnBeforeLoad -= Load;
    }

    void Load(SaveData saveData)
    {
        foreach (var followerData in saveData.sceneData.followersData)
        {
            var follower =  Instantiate(followerPrefab, followerData.Value.followerPosition, followerData.Value.followerRotation);
            follower.GetComponent<GameObjectId>().Id = followerData.Key;
        }
    }
}