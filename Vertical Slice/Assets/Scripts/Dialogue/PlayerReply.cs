using System;
using UnityEngine;
using Unity.VisualScripting;

[Serializable][Inspectable]
public class PlayerReply
{
    //---------------------------------------------------------------------
    // Variables
    //---------------------------------------------------------------------
    [Inspectable] public string line;
    [Inspectable] public DialogueNode nextNode;
}