﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class QuizzManager : MonoBehaviour
{
    public GameObject m_QuizzCanvas;
    public Player m_playerScript;
    private DialogueManager m_dialogueManager;


    public string m_FilePath;

    public TMPro.TextMeshProUGUI m_question;
    public GameObject[] m_buttons;
    public TMPro.TextMeshProUGUI[] m_answers;

    private Quizz m_actualQuizz;

    private int m_actualQuestionIndex = 0;
    public float m_questionsNbr = 5;
    private float m_totalQuestionsNbr = 20;
    private List<int> m_latestQuestionsIndex;

    private List<int> m_randomQuestionsAnswer;
    private int m_rand;

    public float m_goodAnswers = 0;

    void Start()
    {
        m_dialogueManager = GetComponent<DialogueManager>();

        m_QuizzCanvas.SetActive(false);
        m_actualQuizz = Quizz.LoadFromFile(Application.dataPath + m_FilePath);
    }

    public void NextQuestion()
    {
        GetNextQuestionIndex();

        if (m_latestQuestionsIndex == null)
        {
            m_latestQuestionsIndex = new List<int>();
        }
        m_latestQuestionsIndex.Add(m_actualQuestionIndex);

        m_question.text = m_actualQuizz.Questions[m_actualQuestionIndex].Name;

        m_randomQuestionsAnswer = new List<int>();

        RandomQuestionAnswer();

    }

    private void RandomQuestionAnswer()
    {
        GenerateRandomIndexAnswer();
        m_answers[m_rand].text = m_actualQuizz.Questions[m_actualQuestionIndex].Answer1;
        GenerateRandomIndexAnswer();
        m_answers[m_rand].text = m_actualQuizz.Questions[m_actualQuestionIndex].Answer2;
        GenerateRandomIndexAnswer();
        m_answers[m_rand].text = m_actualQuizz.Questions[m_actualQuestionIndex].Answer3;
        GenerateRandomIndexAnswer();
        m_answers[m_rand].text = m_actualQuizz.Questions[m_actualQuestionIndex].Answer4;
    }

    private void GenerateRandomIndexAnswer()
    {
        if(m_rand == 2)
        {
            m_rand += 1;
        }
        else 
        {
            m_rand = Random.Range(0, 3);
        }
        

        for (int i = 0; i < m_randomQuestionsAnswer.Count; i++)
        {
            if (m_randomQuestionsAnswer[i] == m_rand)
            {
                GenerateRandomIndexAnswer();
                return;
            }
        }

        m_randomQuestionsAnswer.Add(m_rand);
    }

    private void GetNextQuestionIndex()
    {
        m_actualQuestionIndex = Random.Range(0, (int)(m_totalQuestionsNbr - 1));

        if(m_latestQuestionsIndex == null)
        {
            return;
        }

        for (int i = 0; i < m_latestQuestionsIndex.Count; i++)
        {
            if (m_latestQuestionsIndex[i] == m_actualQuestionIndex)
            {
                GetNextQuestionIndex();
                return;
            }
        }
    }

    public void CheckAnswer(TMPro.TextMeshProUGUI buttonText)
    {
        if(buttonText.text == m_actualQuizz.Questions[m_actualQuestionIndex].AnswerText)
        {
            m_goodAnswers += 1;
        }

        if (m_latestQuestionsIndex.Count < 5)
        {
            NextQuestion();
        }
        else
        {
            m_question.text = "GG";

            for(int i=0; i< 4; i++)
            {
                m_buttons[i].SetActive(false);
            }
        }
    }

    public void ActivateQuizz()
    {
        m_QuizzCanvas.SetActive(true);
        m_dialogueManager.EndDialogue();
        m_playerScript.enabled = false;
        NextQuestion();
    }
}
