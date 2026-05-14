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
    [SerializeField] private DialogueNode defaultLine;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject replyButtonPrefab;
    [SerializeField] private Transform replyParent;
    [SerializeField] private Image favorImage;
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private NPCFollow npcFollow;
    [SerializeField] private GameObject friendshipLevelUI;
    [SerializeField] private GameObject foxBox;
    [SerializeField] private GameObject playerBox;
    [SerializeField] private GameObject gameController; 

    private DialogueNode currentNode;
    private int currentLineIndex = 0;
    private bool isWaitingForReply = false;
    private bool isShowingPlayerLine = false;
    private int favorLevel = 0;
    private bool firstDialogueFinished = false;
    public bool isInDialogue = false;

    
    private void Update()
    {
        if (!isInDialogue)
        {
            return;
        }

        if (isWaitingForReply)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ShowCurrentLine();
        }
    }
    
    public void StartDialogue()
    {
        Debug.Log("Start Dialogue");

        dialogueUI.SetActive(true);

        isInDialogue = true;
        isWaitingForReply = false;
        isShowingPlayerLine = false;

        if (!firstDialogueFinished)
        {
            currentNode = startLine;
        }
        else
        {
            currentNode = defaultLine;
        }

        currentLineIndex = 0;

        ClearReplies();
        SetDialogueBox(false);
        ShowCurrentLine();

        CustomEvent.Trigger(gameController, "EnterDialogue");
    }

    public void ShowCurrentLine()
    {
        if (currentNode == null) return;

        if (isShowingPlayerLine)
        {
            isShowingPlayerLine = false;
        }

        if (currentLineIndex < currentNode.Lines.Length)
        {
            SetDialogueBox(false);
            dialogueText.text = currentNode.Lines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            isWaitingForReply = true;
            ShowReplies();
        }
    }


    public void ShowReplies()
    {
        ClearReplies();

        if (currentNode.ReplyOptions == null || currentNode.ReplyOptions.Count == 0)
        {
            EndDialogue();
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
        PlayerReply reply = currentNode.ReplyOptions[index];

        favorLevel += reply.favorChange;
        UpdateFavorUI();

        isWaitingForReply = false;
        ClearReplies();

        if (reply.endDialogue)
        {
            EndDialogue();
            return;
        }

        currentNode = reply.nextNode;
        currentLineIndex = 0;

        if (!string.IsNullOrEmpty(reply.playerLine))
        {
            SetDialogueBox(true);
            dialogueText.text = reply.playerLine;
            isShowingPlayerLine = true;
            return;
        }

        ShowCurrentLine();
    }

    private void SetDialogueBox(bool isPlayer)
    {
        if (foxBox != null)
        {
            foxBox.SetActive(!isPlayer);
        }

        if (playerBox != null)
        {
            playerBox.SetActive(isPlayer);
        }
    }

    private void ClearReplies()
    {
        foreach (Transform child in replyParent)
        {
            Destroy(child.gameObject);
        }
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
        isShowingPlayerLine = false;

        ClearReplies();
        dialogueText.text = "";

        if (npcFollow != null)
        {
            npcFollow.EnableFollow();
        }

        CustomEvent.Trigger(gameController, "ExitDialogue");

        if (!firstDialogueFinished)
        {
            firstDialogueFinished = true;

            if (friendshipLevelUI != null)
            {
                friendshipLevelUI.SetActive(true);
            }
        }
    }
}