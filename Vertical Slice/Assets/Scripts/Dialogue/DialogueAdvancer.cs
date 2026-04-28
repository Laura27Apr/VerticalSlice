using UnityEngine;
using Unity.VisualScripting;
using TMPro;

public class DialogueAdvancer : MonoBehaviour
{
    public static DialogueAdvancer _Instance { get; private set; }

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this);
            return;
        }
        _Instance = this;
    }

    [SerializeField] private DialogueNode startLine;
    [SerializeField] private TMP_Text dialogueText;

    private DialogueNode currentNode;
    private int currentLineIndex = 0;

    public void StartDialogue()
    {
        Debug.Log("Start Dialogue");

        currentNode = startLine;
        currentLineIndex = 0;

        ShowCurrentLine();
    }
    public void ShowCurrentLine()
    {
        if (currentNode == null) return;

        if (currentLineIndex < currentNode.Lines.Length)
        {
            dialogueText.text = currentNode.Lines[currentLineIndex];

            currentLineIndex++;
        }

    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            ShowCurrentLine();
        }
    }

}
