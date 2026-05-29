using UnityEngine;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

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

    [Header("Dialogue Nodes")]
    [SerializeField] private DialogueNode startLine;
    [SerializeField] private DialogueNode afterGiftLine;
    [SerializeField] private DialogueNode finalTruthLine;
    [SerializeField] private DialogueNode commonDefaultDialogue;
    [SerializeField] private DialogueNode hintDefaultDialogue;

    [Header("UI")]
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject replyButtonPrefab;
    [SerializeField] private Transform replyParent;
    [SerializeField] private Image favorImage;
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private GameObject friendshipLevelUI;
    [SerializeField] private GameObject foxBox;
    [SerializeField] private GameObject playerBox;

    [Header("Other Systems")]
    [SerializeField] private NPCFollow npcFollow;
    [SerializeField] private GameObject gameController;
    [SerializeField] private BookInteract bookInteract;
    [SerializeField] private GiftGroupInteract giftGroupInteract;
    [SerializeField] private NotebookInteract notebookInteract;
    [SerializeField] private AssistantNotebookInteract assistantNotebookInteract;
    [SerializeField] private GameObject resetBlackScreen;
    [SerializeField] private float resetDelay = 1.5f;

    private DialogueNode currentNode;
    private int currentLineIndex = 0;
    private bool isWaitingForReply = false;
    private bool isShowingPlayerLine = false;

    private int favorLevel = 0;
    private bool friendshipUIShown = false;
    private int storyStage = 0;

    private bool isAfterGiftDialoguePlaying = false;
    private bool isFinalDialoguePlaying = false;
    private bool isHintDefaultDialoguePlaying = false;

    public bool isInDialogue = false;

    private bool hintDialogueFinished = false;
    private int defaultDialogueCount = 0;

    private HashSet<string> readClues = new HashSet<string>();

    private void Start()
    {
        if (friendshipLevelUI != null)
        {
            friendshipLevelUI.SetActive(false);
        }

        if (resetBlackScreen != null)
        {
            resetBlackScreen.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isInDialogue) return;
        if (isWaitingForReply) return;

        if (Input.GetMouseButtonDown(0))
        {
            ShowCurrentLine();
        }
    }

    public void StartDialogue()
    {
        Debug.Log("Start Dialogue");

        dialogueUI.SetActive(true);

        if (!friendshipUIShown && friendshipLevelUI != null)
        {
            friendshipLevelUI.SetActive(true);
            friendshipUIShown = true;
        }

        isInDialogue = true;
        isWaitingForReply = false;
        isShowingPlayerLine = false;

        isAfterGiftDialoguePlaying = false;
        isFinalDialoguePlaying = false;
        isHintDefaultDialoguePlaying = false;

        if (storyStage == 0)
        {
            currentNode = startLine;
        }
        else if (storyStage == 2)
        {
            currentNode = afterGiftLine;
            isAfterGiftDialoguePlaying = true;
        }
        else if (storyStage == 3)
        {
            currentNode = finalTruthLine;
            isFinalDialoguePlaying = true;
        }
        else
        {
            if (defaultDialogueCount == 0)
            {
                currentNode = commonDefaultDialogue;
            }
            else if (!hintDialogueFinished)
            {
                currentNode = hintDefaultDialogue;
                isHintDefaultDialoguePlaying = true;
            }
            else
            {
                currentNode = commonDefaultDialogue;
            }

            defaultDialogueCount++;
        }

        currentLineIndex = 0;

        ClearReplies();
        SetDialogueBox(false);
        ShowCurrentLine();

        if (gameController != null)
        {
            CustomEvent.Trigger(gameController, "EnterDialogue");
        }
    }

    public void FoundGift()
    {
        if (storyStage < 2)
        {
            storyStage = 2;
        }
    }

    public void FoundDesk()
    {
        if (storyStage < 3)
        {
            storyStage = 3;
        }
    }

    public void MarkClueRead(string clueID)
    {
        if (!string.IsNullOrEmpty(clueID))
        {
            readClues.Add(clueID);
            Debug.Log("Clue read: " + clueID);
        }
    }

    private bool HasReadClue(string clueID)
    {
        return string.IsNullOrEmpty(clueID) || readClues.Contains(clueID);
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

            if (!HasReadClue(reply.requiredClueID))
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

        if (reply.unlockBookAfterReply)
        {
            if (bookInteract != null)
            {
                bookInteract.UnlockBookInteract();
            }

            if (notebookInteract != null)
            {
                notebookInteract.UnlockOutline();
            }

            if (assistantNotebookInteract != null)
            {
                assistantNotebookInteract.UnlockOutline();
            }

            storyStage = 1;
        }

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
        if (favorImage == null) return;

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

        if (isHintDefaultDialoguePlaying)
        {
            hintDialogueFinished = true;
            isHintDefaultDialoguePlaying = false;

            if (notebookInteract != null)
            {
                notebookInteract.UnlockOutline();
            }

            if (assistantNotebookInteract != null)
            {
                assistantNotebookInteract.UnlockOutline();
            }
        }

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

        if (gameController != null)
        {
            CustomEvent.Trigger(gameController, "ExitDialogue");
        }

        if (storyStage == 0)
        {
            storyStage = 1;

            if (giftGroupInteract != null)
            {
                giftGroupInteract.UnlockGiftGroupInteract();
            }
        }

        if (isAfterGiftDialoguePlaying)
        {
            isAfterGiftDialoguePlaying = false;
        }

        if (isFinalDialoguePlaying)
        {
            StartCoroutine(ResetGameAfterFinalDialogue());
            return;
        }
    }

    private IEnumerator ResetGameAfterFinalDialogue()
    {
        if (resetBlackScreen != null)
        {
            resetBlackScreen.SetActive(true);
        }

        yield return new WaitForSeconds(resetDelay);

        SceneManager.LoadScene("OpeningScene");
    }
}