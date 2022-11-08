using UnityEngine;
using UnityEngine.AI;

public class AgentAnimatorMover : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private NavMeshAgent agent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (agent == null)
            agent = GetComponentInParent<NavMeshAgent>();
    }

    private void OnAnimatorMove()
    {
        if (!animator.applyRootMotion) return;
        var velocity = animator.deltaPosition;
        if (agent == null) return;
        if (agent.enabled == false) return;
        agent.Move(velocity);
    }
}