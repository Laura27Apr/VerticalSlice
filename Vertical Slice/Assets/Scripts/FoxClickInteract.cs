using UnityEngine;
using Unity.VisualScripting;

public class FoxClickInteract : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private DialogueAdvancer dialogueAdvancer;
    [SerializeField] private float interactDistance = 3f;

    private void Update()
    {
        if (dialogueAdvancer == null || player == null) return;

        if (dialogueAdvancer.isInDialogue) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactDistance && Input.GetMouseButtonDown(0))
        {
            dialogueAdvancer.StartDialogue();
        }
    }
}