using UnityEngine;

public class FollowerDestination : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private Transform rotateTarget;
    private bool destSet;
    private AIFollower followerAI;

    private void OnTriggerEnter(Collider other)
    {
        var follower = other.GetComponent<AIFollower>();
        if (follower == null) return;
        followerAI = follower;

        if (followerAI.currentTarget == null) return;
        var playerFollower = followerAI.currentTarget.GetComponent<PlayerFollower>();
        var questManager = followerAI.currentTarget.GetComponent<QuestManager>();
        playerFollower.RemoveFollower();
        questManager.TryCompleteCurrentStep(InteractionType.TriggerInteraction, followerAI.WorldEntity,
            followerAI.gameObject);
        var position = destination.position;
        followerAI.navMeshAgent.stoppingDistance = .1f;
        followerAI.navMeshAgent.SetDestination(position);

        destSet = true;
    }

    [SerializeField] private float destReachedDistance = 3f;

    private bool startRotating;

    private void Update()
    {
        if (destSet)
        {
            if (followerAI == null) return;
            if (Vector3.Distance(destination.position, followerAI.transform.position) <= destReachedDistance)
            {
                followerAI.navMeshAgent.velocity = Vector3.zero;
                followerAI.navMeshAgent.ResetPath();
                destSet = false;
                startRotating = true;
            }
        }

        if (startRotating)
        {
            var rotationFinished = followerAI.LookToTransform(rotateTarget);
            if (rotationFinished)
            {
                startRotating = false;
                var transform1 = followerAI.transform;
                SaveData.Current.sceneData.followersData[followerAI.GetComponent<GameObjectId>().Id] =
                    new FollowerData
                    {
                        followerPosition = transform1.position,
                        followerRotation = transform1.rotation
                    };
            }
        }
    }
}