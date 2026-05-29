using UnityEngine;
using Unity.VisualScripting;

public class GiftGroupInteract : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private GameObject readUI;
    [SerializeField] private Transform player;
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private CluePageSwitcher pageSwitcher;
    [SerializeField] private string clueID;

    private bool playerInteract;
    private bool unlocked = false;
    private bool giftAlreadyRead = false;

    private void Start()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }

        if (readUI != null)
        {
            readUI.SetActive(false);
        }
    }

    public void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        playerInteract = distance <= interactDistance;

        promptUI.SetActive(unlocked && playerInteract && !readUI.activeSelf);

        if (unlocked && playerInteract && promptUI.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            readUI.SetActive(true);

            if (pageSwitcher != null)
            {
                pageSwitcher.ShowShortPage();
            }

            if (!giftAlreadyRead)
            {
                giftAlreadyRead = true;

                if (DialogueAdvancer._Instance != null)
                {
                    DialogueAdvancer._Instance.MarkClueRead(clueID);
                    DialogueAdvancer._Instance.FoundGift();
                }
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