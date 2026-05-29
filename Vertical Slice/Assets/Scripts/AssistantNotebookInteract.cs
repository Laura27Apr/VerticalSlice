using UnityEngine;
using Unity.VisualScripting;

public class AssistantNotebookInteract : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private GameObject readUI;
    [SerializeField] private Transform player;
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private CluePageSwitcher pageSwitcher;
    [SerializeField] private string clueID;
    [SerializeField] private GameObject outlineTarget;
    [SerializeField] private string outlineLayerName = "Outline";
    [SerializeField] private string normalLayerName = "Default";

    private bool playerInRange;
    private bool assistantNotebookalreadyRead = false;

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

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        playerInRange = distance <= interactDistance;

        promptUI.SetActive(playerInRange && !readUI.activeSelf);

        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            readUI.SetActive(true);
            promptUI.SetActive(false);

            if (pageSwitcher != null)
            {
                pageSwitcher.ShowShortPage();
            }

            Variables.ActiveScene.Set("isReading", true);

            if (!assistantNotebookalreadyRead)
            {
                assistantNotebookalreadyRead = true;

                if (DialogueAdvancer._Instance != null)
                {
                    DialogueAdvancer._Instance.MarkClueRead(clueID);
                }

                if (outlineTarget != null)
                {
                    SetLayer(outlineTarget, LayerMask.NameToLayer(normalLayerName));
                }
            }

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

    public void UnlockOutline()
    {
        if (!assistantNotebookalreadyRead && outlineTarget != null)
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