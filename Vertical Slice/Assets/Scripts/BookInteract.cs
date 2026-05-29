using UnityEngine;
using Unity.VisualScripting;

public class BookInteract : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private GameObject readUI;
    [SerializeField] private Transform player;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private CluePageSwitcher pageSwitcher;
    [SerializeField] private string clueID;
    [SerializeField] private GameObject fireObject;
    [SerializeField] private GameObject outlineTarget;
    [SerializeField] private string outlineLayerName = "Outline";
    [SerializeField] private string normalLayerName = "Default";

    private bool playerInteract;
    private bool unlocked = false;
    private bool bookAlreadyRead = false;

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

        if (fireObject != null)
        {
            fireObject.SetActive(false);
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
                    SetLayerRecursively(outlineTarget, LayerMask.NameToLayer(normalLayerName));
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

    public void UnlockBookInteract()
    {
        unlocked = true;

        if (fireObject != null)
        {
            fireObject.SetActive(true);
        }

        if (!bookAlreadyRead && outlineTarget != null)
        {
            SetLayerRecursively(outlineTarget, LayerMask.NameToLayer(outlineLayerName));
        }
    }

    private void SetLayerRecursively(GameObject obj, int layer)
    {
        if (layer < 0) return;

        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}