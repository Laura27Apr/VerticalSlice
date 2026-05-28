using UnityEngine;
using Unity.VisualScripting;

public class OtherReadableInteract : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private GameObject readUI;
    [SerializeField] private Transform player;
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private CluePageSwitcher pageSwitcher;
    [SerializeField] private GameObject outlineTarget;
    [SerializeField] private string normalLayerName = "Default";

    private bool playerInRange;
    private bool alreadyRead = false;

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

            if (!alreadyRead)
            {
                alreadyRead = true;

                if (outlineTarget != null)
                {
                    SetLayerRecursively(outlineTarget, LayerMask.NameToLayer(normalLayerName));
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