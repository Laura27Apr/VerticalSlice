using UnityEngine;
using Unity.VisualScripting;

public class DialogueAdvancer : MonoBehaviour
{
    [SerializeField] private DialogueNode nextLine;

    // Button hooks up to this method
    public void ChooseDialogue()
    {
        EventBus.Trigger(EventNames.NewDialogueEvent, nextLine);
    }

    public void PrintHello()
    {
        Debug.Log("hello!");
    }
}
