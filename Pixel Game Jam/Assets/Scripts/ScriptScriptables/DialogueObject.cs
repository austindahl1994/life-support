using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueObject : ScriptableObject
{
    [TextArea(5, 10)]
    public string[] dialogueText;
}
