using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLine", menuName = "ScriptableObjects/DialogueLine", order = 1)]
public class DialogueNode : ScriptableObject
{
    //---------------------------------------------------------------------
    // Variables
    //---------------------------------------------------------------------
    [Tooltip("The line of dialogue the NPC says in this node.")]
    public string Line;

    [Tooltip("[Optional] The dialogue options for the player's response, and what node each response leads to.")]
    public List<PlayerReply> ReplyOptions;
}