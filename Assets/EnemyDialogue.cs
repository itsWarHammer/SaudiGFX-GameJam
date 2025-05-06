using UnityEngine;
using System;

[System.Serializable]
public class EnemyDialogue
{
    public string[] dialogues;
    public enum Emotion { Angry, Sad, Happy }
    public Emotion emotion;
}
