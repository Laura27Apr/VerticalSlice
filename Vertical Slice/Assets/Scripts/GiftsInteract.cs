using UnityEngine;
using Unity.VisualScripting;

public class GiftGroupInteract : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private GameObject readUI;
    [SerializeField] private Transform player;
    [SerializeField] private float interactDistance = 3f;

    private bool playerInteract;
    private bool unlocked = false;
    private bool giftAlreadyRead = false;

    public void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        playerInteract = distance <= interactDistance;

        promptUI.SetActive(unlocked && playerInteract && !readUI.activeSelf);

        if (unlocked && playerInteract && promptUI.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            readUI.SetActive(true);

            if (!giftAlreadyRead)
            {
                giftAlreadyRead = true;
                DialogueAdvancer._Instance.FoundGift();
            }

            Variables.ActiveScene.Set("isReading", true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            promptUI.SetActive(false);
        }

        if (readUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            readUI.SetActive(false);

            Variables.ActiveScene.Set("isReading", false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (unlocked && playerInteract)
            {
                promptUI.SetActive(true);
            }
        }
    }

    public void UnlockGiftGroupInteract()
    {
        unlocked = true;
    }
}