using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [Header("Music")]
    public AudioSource m_audioSource;
    public AudioClip m_sound;

    [Header("Dialogue")]
    public bool m_randomSentence = false;
    public float m_speedDialogue = 0;
    public string m_name;

    [TextArea(3, 10)]
    public string[] sentences;
}
