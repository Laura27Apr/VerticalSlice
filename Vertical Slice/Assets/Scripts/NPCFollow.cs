using UnityEngine;
using UnityEngine.AI;

public class NPCFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followDistance = 2f;
    
    private NavMeshAgent agent;
    private bool followStart = false;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }

    public void Update()
    {
        
        bool isInDialogue = DialogueAdvancer._Instance.isInDialogue;

        float distance = Vector3.Distance(transform.position, player.position);

        if (followStart && !isInDialogue && distance > followDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            Debug.Log("NPC is following player");
        }
        else
        {
            agent.isStopped = true;

            Debug.Log("NPC stopped");
        }
    }
    public void EnableFollow()
    {
        followStart = true;
        Debug.Log("NPC follow unlocked");
    }
}
