using UnityEngine;

public class BookInteract : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private GameObject readUI;
    [SerializeField] private Transform player;
    [SerializeField] private float interactDistance = 2f;

    private bool playerInteract;

    public void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        playerInteract = distance <= interactDistance;

        promptUI.SetActive(playerInteract && !readUI.activeSelf);

        if (playerInteract && Input.GetKeyDown(KeyCode.F))
        {
            readUI.SetActive(true);
            promptUI.SetActive(false);
            Debug.Log("F pressed");
        }

        if (readUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            readUI.SetActive(false);

            if (playerInteract)
            {
                promptUI.SetActive(true);
            }

        }
    }

   
}