using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;

public class NPCFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followDistance = 4f;
    [SerializeField] private float stopDistance = 2.5f;
    [SerializeField] private float unlockDelay = 0.5f;

    private NavMeshAgent agent;
    private bool followStart = false;
    private bool wasMoving = false;
    private bool justFinishedDialogue = false;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        agent.ResetPath();
    }

    public void Update()
    {
        if (justFinishedDialogue)
        {
            agent.isStopped = true;
            agent.ResetPath();

            CustomEvent.Trigger(gameObject, "StopFollow");
            return;
        }

        bool isInDialogue = DialogueAdvancer._Instance.isInDialogue;
        float distance = Vector3.Distance(transform.position, player.position);

        bool shouldMove = false;

        if (followStart && !isInDialogue)
        {
            if (!wasMoving && distance > followDistance)
            {
                shouldMove = true;
            }
            else if (wasMoving && distance > stopDistance)
            {
                shouldMove = true;
            }
        }

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
            agent.ResetPath();

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
        agent.isStopped = true;
        agent.ResetPath();

        followStart = false;
        wasMoving = false;
        justFinishedDialogue = true;

        CustomEvent.Trigger(gameObject, "StopFollow");

        Invoke(nameof(ActuallyEnableFollow), unlockDelay);

        Debug.Log("NPC follow will unlock after delay");
    }

    private void ActuallyEnableFollow()
    {
        followStart = true;
        justFinishedDialogue = false;

        agent.isStopped = true;
        agent.ResetPath();

        CustomEvent.Trigger(gameObject, "StopFollow");

        Debug.Log("NPC follow unlocked");
    }
}