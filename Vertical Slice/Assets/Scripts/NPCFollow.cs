using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;

public class NPCFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followDistance = 2f;

    private NavMeshAgent agent;
    private bool followStart = false;
    private bool wasMoving = false;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }

    public void Update()
    {
        bool isInDialogue = DialogueAdvancer._Instance.isInDialogue;
        float distance = Vector3.Distance(transform.position, player.position);

        bool shouldMove = followStart && !isInDialogue && distance > followDistance;

        if (shouldMove)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            if (!wasMoving)
            {
                Debug.Log("Trigger StartFollow");
                CustomEvent.Trigger(gameObject, "StartFollow");
                wasMoving = true;
            }
        }
        else
        {
            agent.isStopped = true;

            if (wasMoving)
            {
                Debug.Log("Trigger StopFollow");
                CustomEvent.Trigger(gameObject, "StopFollow");
                wasMoving = false;
            }
        }
    }

    public void EnableFollow()
    {
        followStart = true;
        Debug.Log("NPC follow unlocked");
    }
}