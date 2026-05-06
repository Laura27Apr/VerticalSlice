using UnityEngine;

public class PromptUIFacing : MonoBehaviour
{
    public void LateUpdate()
    {
        if (Camera.main == null) return;

        Vector3 direction = Camera.main.transform.position - transform.position;

        direction.y = 0;
        
        transform.rotation = Quaternion.LookRotation(direction);
    }
}