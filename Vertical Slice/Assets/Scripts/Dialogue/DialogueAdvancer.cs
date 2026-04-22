using UnityEngine;
using Unity.VisualScripting;

public class DialogueAdvancer : MonoBehaviour
{
    [SerializeField] private DialogueNode nextLine;

    /*public void Start()
    {
        EventBus.Trigger(EventNames.NewDialogueEvent, nextLine);
    }/*

    // Button hooks up to this method
    /*public void ChooseDialogue()
    {
        EventBus.Trigger(EventNames.NewDialogueEvent, nextLine);
    }

    public void PrintHello()
    {
        Debug.Log("hello!");
    }*/
}
