using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class QuizzManager : MonoBehaviour
{
    public GameObject m_QuizzCanvas;
    public Player m_playerScript;


    public string m_FilePath;

    public TMPro.TextMeshProUGUI m_question;
    public TMPro.TextMeshProUGUI[] m_answers;

    private Quizz m_actualQuizz;

    private int m_actualQuestionIndex = 0;
    public float m_questionsNbr = 5;
    private float m_totalQuestionsNbr = 20;
    private List<int> m_latestQuestionsIndex;
    private float m_goodAnswers = 0;

    void Start()
    {
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

        m_answers[0].text = m_actualQuizz.Questions[m_actualQuestionIndex].Answer1;
        m_answers[1].text = m_actualQuizz.Questions[m_actualQuestionIndex].Answer2;
        m_answers[2].text = m_actualQuizz.Questions[m_actualQuestionIndex].Answer3;
        m_answers[3].text = m_actualQuizz.Questions[m_actualQuestionIndex].Answer4;
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

    public void CheckAnswer(int buttonIndex)
    {
        if(buttonIndex == m_actualQuizz.Questions[0].AnswerIndex)
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

            m_answers[0].text = "";
            m_answers[1].text = "";
            m_answers[2].text = "";
            m_answers[3].text = "";
        }
    }

    public void ActivateQuizz()
    {
        m_QuizzCanvas.SetActive(true);
        m_playerScript.enabled = false;

        NextQuestion();
    }
}
