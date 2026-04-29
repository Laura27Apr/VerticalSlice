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
    [SerializeField] private Image favorImage;
    [SerializeField] private GameObject dialogueUI;

    private DialogueNode currentNode;
    private int currentLineIndex = 0;
    private bool isWaitingForReply = false;
    private int favorLevel = 0;

    public bool isInDialogue = false;

    public void StartDialogue()
    {
        Debug.Log("Start Dialogue");

        isInDialogue = true;
        currentNode = startLine;
        currentLineIndex = 0;

        ShowCurrentLine();
        CustomEvent.Trigger(gameObject, "EnterDialogue");
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
            isInDialogue = false;
            return;
        }


        for (int i = 0; i < currentNode.ReplyOptions.Count; i++)
        {
            PlayerReply reply = currentNode.ReplyOptions[i];

            if (favorLevel < reply.requiredFavor)
            {
                continue;
            }

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

        PlayerReply reply = currentNode.ReplyOptions[index];

        favorLevel += currentNode.ReplyOptions[index].favorChange;
        UpdateFavorUI();

        if (reply.endDialogue)
        {
            EndDialogue();
            return;
        }

        currentNode = currentNode.ReplyOptions[index].nextNode;
        currentLineIndex = 0;

        foreach (Transform child in replyParent)
        {
            Destroy(child.gameObject);
        }

        ShowCurrentLine();
    }

    public void UpdateFavorUI()
    {
        if (favorLevel > 0)
        {
            favorImage.color = new Color(1f, 0.71f, 0.76f); 
        }
        else if (favorLevel < 0)
        {
            favorImage.color = new Color(0.4f, 0.4f, 0.4f);
        }
        else
        {
            favorImage.color = new Color(1f, 1f, 1f);
        }
    }

    public void EndDialogue()
    {
        Debug.Log("Dialogue End");

        isInDialogue = false;
        dialogueUI.SetActive(false);

        currentNode = null;
        currentLineIndex = 0;
        isWaitingForReply = false;

        foreach (Transform child in replyParent)
        {
            Destroy(child.gameObject);
        }

        dialogueText.text = "";
        CustomEvent.Trigger(gameObject, "ExitDialogue");
    }
}
