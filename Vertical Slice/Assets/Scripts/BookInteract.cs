using UnityEngine;
using Unity.VisualScripting;

public class BookInteract : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private GameObject readUI;
    [SerializeField] private GameObject lockedClueUI;
    [SerializeField] private Transform player;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private CluePageSwitcher pageSwitcher;
    [SerializeField] private string clueID;
    [SerializeField] private GameObject fireObject;
    [SerializeField] private GameObject outlineTarget;
    [SerializeField] private string outlineLayerName = "Outline";
    [SerializeField] private string normalLayerName = "Default";
    [SerializeField] private bool isLastClue = false;

    private bool playerInteract;
    private bool canShowPrompt = false;
    private bool canReadBook = false;
    private bool bookAlreadyRead = false;
    private bool lockedMessageOpen = false;

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

        if (lockedClueUI != null)
        {
            lockedClueUI.SetActive(false);
        }

        if (fireObject != null)
        {
            fireObject.SetActive(false);
        }
    }

    public void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        playerInteract = distance <= interactDistance;

        promptUI.SetActive(canShowPrompt && playerInteract && !readUI.activeSelf && !lockedMessageOpen);

        if (lockedMessageOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            if (lockedClueUI != null)
            {
                lockedClueUI.SetActive(false);
            }

            lockedMessageOpen = false;

            Variables.ActiveScene.Set("isReading", false);

            if (DialogueAdvancer._Instance != null)
            {
                DialogueAdvancer._Instance.ShowFriendshipUI();
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (canShowPrompt && playerInteract)
            {
                promptUI.SetActive(true);
            }

            return;
        }

        if (canShowPrompt && playerInteract && promptUI.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            if (!canReadBook)
            {
                if (lockedClueUI != null)
                {
                    lockedClueUI.SetActive(true);
                }

                lockedMessageOpen = true;

                Variables.ActiveScene.Set("isReading", true);

                if (DialogueAdvancer._Instance != null)
                {
                    DialogueAdvancer._Instance.HideFriendshipUI();
                }

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                promptUI.SetActive(false);

                return;
            }

            if (lockedClueUI != null)
            {
                lockedClueUI.SetActive(false);
            }

            readUI.SetActive(true);

            Variables.ActiveScene.Set("isReading", true);

            if (DialogueAdvancer._Instance != null)
            {
                DialogueAdvancer._Instance.HideFriendshipUI();
            }

            if (isLastClue && Music.Instance != null)
            {
                Music.Instance.PlayFinalBookBGM();
            }

            if (pageSwitcher != null)
            {
                pageSwitcher.ShowShortPage();
            }

            if (!bookAlreadyRead)
            {
                bookAlreadyRead = true;

                if (DialogueAdvancer._Instance != null)
                {
                    DialogueAdvancer._Instance.MarkClueRead(clueID);
                    DialogueAdvancer._Instance.FoundDesk();
                }

                if (outlineTarget != null)
                {
                    SetLayer(outlineTarget, LayerMask.NameToLayer(normalLayerName));
                }
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            promptUI.SetActive(false);
        }

        if (readUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            readUI.SetActive(false);

            Variables.ActiveScene.Set("isReading", false);

            if (DialogueAdvancer._Instance != null)
            {
                DialogueAdvancer._Instance.ShowFriendshipUI();
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (canShowPrompt && playerInteract)
            {
                promptUI.SetActive(true);
            }
        }

        if (!playerInteract && lockedClueUI != null)
        {
            lockedClueUI.SetActive(false);
            lockedMessageOpen = false;

            if (!readUI.activeSelf)
            {
                Variables.ActiveScene.Set("isReading", false);

                if (DialogueAdvancer._Instance != null)
                {
                    DialogueAdvancer._Instance.ShowFriendshipUI();
                }
            }
        }
    }

    public void ShowBookPrompt()
    {
        canShowPrompt = true;

        if (fireObject != null)
        {
            fireObject.SetActive(true);
        }

        if (!bookAlreadyRead && outlineTarget != null)
        {
            SetLayer(outlineTarget, LayerMask.NameToLayer(outlineLayerName));
        }
    }

    public void UnlockBookInteract()
    {
        canShowPrompt = true;
        canReadBook = true;

        if (lockedClueUI != null)
        {
            lockedClueUI.SetActive(false);
        }

        if (fireObject != null)
        {
            fireObject.SetActive(true);
        }

        if (!bookAlreadyRead && outlineTarget != null)
        {
            SetLayer(outlineTarget, LayerMask.NameToLayer(outlineLayerName));
        }
    }

    private void SetLayer(GameObject obj, int layer)
    {
        if (layer < 0) return;

        obj.layer = layer;
    }
}