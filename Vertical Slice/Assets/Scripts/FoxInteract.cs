using UnityEngine;
using Unity.VisualScripting;

public class FoxClickInteract : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private DialogueAdvancer dialogueAdvancer;
    [SerializeField] private GameObject promptUI;
    [SerializeField] private float interactDistance = 3f;

    private bool playerInRange;

    private void Start()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (dialogueAdvancer == null || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        playerInRange = distance <= interactDistance;

        if (promptUI != null)
        {
            promptUI.SetActive(playerInRange && !dialogueAdvancer.isInDialogue);
        }

        if (playerInRange && !dialogueAdvancer.isInDialogue && Input.GetKeyDown(KeyCode.T))
        {
            if (promptUI != null)
            {
                promptUI.SetActive(false);
            }

            dialogueAdvancer.StartDialogue();
        }
    }
}