﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Player m_playerScript;

    [Header("Dialogue")]
    public TMPro.TextMeshProUGUI m_nameText;
    public TMPro.TextMeshProUGUI m_dialogueText;
    public GameObject m_dialogueBox;
    private Queue<string> sentences;
    private float m_speedDialogue = 0;


    private AudioSource m_audioSource;
    private AudioClip m_sound;

    void Start()
    {
        m_dialogueBox.SetActive(false);
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        m_audioSource = dialogue.m_audioSource;
        m_sound = dialogue.m_sound;

        
        m_speedDialogue = dialogue.m_speedDialogue;
        m_playerScript.enabled = false;
        //animator.SetBool("IsOpen", true);
        m_dialogueBox.SetActive(true);
        m_nameText.text = dialogue.m_name;

        sentences.Clear();

        if(dialogue.m_randomSentence)
        {
            int rand = Random.Range(0, dialogue.sentences.Length);
            sentences.Enqueue(dialogue.sentences[rand]);
        }
        else
        {
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        m_audioSource.PlayOneShot(m_sound);
        string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        m_dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            m_dialogueText.text += letter;
            yield return new WaitForSeconds(m_speedDialogue);
        }

        while(!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        DisplayNextSentence();
    }

    public void EndDialogue()
    {
        m_speedDialogue = 0;
        m_playerScript.enabled = true;
        //animator.SetBool("IsOpen", false);
        m_dialogueBox.SetActive(false);
    }
}
