using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{


    public TMPro.TextMeshProUGUI m_nameText;
    public TMPro.TextMeshProUGUI m_dialogueText;

    //public Animator animator;

    public GameObject m_dialogueBox;

    public float time_new_sentence = 3f;


    private Queue<string> sentences;

    void Start()
    {
        m_dialogueBox.SetActive(false);
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //animator.SetBool("IsOpen", true);
        m_dialogueBox.SetActive(true);
        m_nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
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
            yield return null;
        }

        yield return new WaitForSeconds(time_new_sentence);
        DisplayNextSentence();
    }

    void EndDialogue()
    {
        //animator.SetBool("IsOpen", false);
        m_dialogueBox.SetActive(false);
    }
}
