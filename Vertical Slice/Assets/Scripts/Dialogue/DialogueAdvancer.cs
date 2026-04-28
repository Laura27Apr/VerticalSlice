using UnityEngine;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;

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
    [SerializeField] private GameObject replyButtonPrefab;
    [SerializeField] private Transform replyParent;

    private DialogueNode currentNode;
    private int currentLineIndex = 0;
    private bool isWaitingForReply = false;

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
        else
        {
            isWaitingForReply = true;
            ShowReplies();
        }

    }

    private void Update()
    {
        if (isWaitingForReply)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ShowCurrentLine();
        }
    }

    public void ShowReplies()
    {
        foreach (Transform child in replyParent)
        {
            Destroy(child.gameObject);
        }

        if (currentNode.ReplyOptions == null || currentNode.ReplyOptions.Count == 0)
        {
            return;
        }


        for (int i = 0; i < currentNode.ReplyOptions.Count; i++)
        {
            PlayerReply reply = currentNode.ReplyOptions[i];
            GameObject button = Instantiate(replyButtonPrefab, replyParent);

            button.GetComponentInChildren<TMP_Text>().text = reply.line;

            int index = i;

            button.GetComponent<Button>().onClick.AddListener(delegate
            {
                SelectReply(index);
            });
        }
    }

    public void SelectReply(int index)
    {
        isWaitingForReply = false;

        currentNode = currentNode.ReplyOptions[index].nextNode;
        currentLineIndex = 0;

        foreach (Transform child in replyParent)
        {
            Destroy(child.gameObject);
        }

        ShowCurrentLine();
    }
}
