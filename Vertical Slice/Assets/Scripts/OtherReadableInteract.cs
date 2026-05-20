using UnityEngine;
using Unity.VisualScripting;

public class OtherReadableInteract : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private GameObject readUI;
    [SerializeField] private Transform player;
    [SerializeField] private float interactDistance = 3f;

    private bool playerInRange;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        playerInRange = distance <= interactDistance;

        promptUI.SetActive(playerInRange && !readUI.activeSelf);

        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            readUI.SetActive(true);
            promptUI.SetActive(false);

            Variables.ActiveScene.Set("isReading", true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (readUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            readUI.SetActive(false);

            Variables.ActiveScene.Set("isReading", false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}