using UnityEngine;

public class PromptUIFacing : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (mainCamera == null) return;

        transform.rotation = mainCamera.transform.rotation;
        transform.Rotate(0f, 180f, 0f);
    }
}