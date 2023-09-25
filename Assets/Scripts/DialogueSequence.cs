using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Sequence", menuName = "Dialogue/Dialogue Sequence")]

public class DialogueSequence : ScriptableObject
{
    public List<DialogueLine> lines;
}

[Serializable]
public class DialogueLine
{
    public string speaker;
    public string text;
    public AudioClip speakingSFX;
}