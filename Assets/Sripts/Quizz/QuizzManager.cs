using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml.Serialization;

public class QuizzManager : MonoBehaviour
{
    [Header("Link Objects")]
    public GameObject m_QuizzCanvas;
    public Player m_playerScript;
    public EventSystem m_eventSystem;
    private DialogueManager m_dialogueManager;

    [Header("Audio")]
    public AudioSource m_audioSource;
    public AudioSource m_musicAudioSource;
    public AudioClip m_goodAnswer;
    public AudioClip m_badAnswer;
    public AudioClip m_clipStart;
    public AudioClip m_perfect;
    public AudioClip m_music;

    [Header("Questions")]
    public string m_FilePath;
    public TMPro.TextMeshProUGUI m_question;
    public GameObject[] m_buttons;
    public TMPro.TextMeshProUGUI[] m_answers;
    public float m_questionsNbr = 5;
    public float m_goodAnswers = 0;
    public float m_waitForNextQuestion = 2;
    public int m_timeForQuestion = 10;
    public bool m_questionIsAnswered = false;
    private GameObject m_lastButtonPressed;
    private Quizz m_actualQuizz;
    private int m_actualQuestionIndex = 0;
    private float m_totalQuestionsNbr = 20;
    private List<int> m_latestQuestionsIndex;
    private List<int> m_randomQuestionsAnswer;
    private int m_rand;

    [Header("Timer")]
    public TMPro.TextMeshProUGUI m_timerTxt;
    public GameObject m_timerObj;
    private float m_timer;

    [Header("Balloons")]
    public TMPro.TextMeshProUGUI m_BalloonTxt;
    public GameObject m_balloonObj;


    void Start()
    {
        m_dialogueManager = GetComponent<DialogueManager>();

        m_QuizzCanvas.SetActive(false);
        m_actualQuizz = Quizz.LoadFromFile(Application.dataPath + m_FilePath);
    }

    public void NextQuestion()
    {
        m_eventSystem.SetSelectedGameObject(m_buttons[0]);

        GetNextQuestionIndex();

        if (m_latestQuestionsIndex == null)
        {
            m_latestQuestionsIndex = new List<int>();
        }
        m_latestQuestionsIndex.Add(m_actualQuestionIndex);

        m_question.text = m_actualQuizz.Questions[m_actualQuestionIndex].Name;

        m_randomQuestionsAnswer = new List<int>();

        RandomQuestionAnswer();

        StartCoroutine(QuestionTime());

    }

    private void GetNextQuestionIndex()
    {
        m_actualQuestionIndex = Random.Range(0, (int)(m_totalQuestionsNbr));

        if (m_latestQuestionsIndex == null)
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
       m_rand = Random.Range(0, 4);

        

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

    public void CheckAnswer(GameObject button)
    {
        StopAllCoroutines();

        m_eventSystem.enabled = false;

        TMPro.TextMeshProUGUI buttonText = button.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        m_lastButtonPressed = button;

        if (buttonText.text == m_actualQuizz.Questions[m_actualQuestionIndex].AnswerText)
        {
            m_goodAnswers += 1;
            Player.m_numberBalloons += 1;
            m_BalloonTxt.text = Player.m_numberBalloons.ToString();
            button.GetComponent<Image>().color = Color.green;
            
            m_audioSource.clip = m_goodAnswer;
            m_audioSource.Play();
        }
        else
        {
            button.GetComponent<Image>().color = Color.red;

            m_audioSource.clip = m_badAnswer;
            m_audioSource.Play();
        }

        if (m_latestQuestionsIndex.Count < m_questionsNbr)
        {
            StartCoroutine(WaitForNextQuestion());
        }
        else
        {
            StartCoroutine(WaitEndQuizz());
            
        }
    }

    public void EndOfTime()
    {
        m_eventSystem.enabled = false;

        m_audioSource.clip = m_badAnswer;
        m_audioSource.Play();
    

        if (m_latestQuestionsIndex.Count<m_questionsNbr)
        {
            StartCoroutine(WaitForNextQuestion());
}
        else
        {
            StartCoroutine(WaitEndQuizz());
        }
    }

    public void ActivateQuizz()
    {
        m_audioSource.Stop();
        m_musicAudioSource.Stop();
        m_audioSource.clip = m_clipStart;
        m_audioSource.Play();
        m_dialogueManager.EndDialogue();
        m_balloonObj.SetActive(false);
        m_playerScript.enabled = false;

        StartCoroutine(WaitStartQuizz());
    }

    public void EndQuizz()
    {
        m_timerObj.SetActive(false);
        m_playerScript.enabled = true;
        m_playerScript.enabled = false;

        if (m_goodAnswers == m_questionsNbr)
        {
            m_musicAudioSource.Stop();
            m_musicAudioSource.PlayOneShot(m_perfect);
        }

        m_question.text = "GG";

        for (int i = 0; i < 4; i++)
        {
            m_buttons[i].SetActive(false);
        }
    }

    IEnumerator WaitStartQuizz()
    {
        yield return new WaitForSecondsRealtime(m_clipStart.length);
        m_balloonObj.SetActive(true);
        m_QuizzCanvas.SetActive(true);
        NextQuestion();
        m_musicAudioSource.clip = m_music;
        m_musicAudioSource.loop = true;
        m_musicAudioSource.Play();
        m_timerObj.SetActive(true);
    }

    IEnumerator WaitEndQuizz()
    {
        yield return new WaitForSeconds(m_waitForNextQuestion);
        if (m_lastButtonPressed)
        {
            m_lastButtonPressed.GetComponent<Image>().color = Color.white;
        }
        m_eventSystem.enabled = true;

        EndQuizz();

    }

    IEnumerator WaitForNextQuestion()
    {
        yield return new WaitForSeconds(m_waitForNextQuestion);
        if (m_lastButtonPressed)
        {
            m_lastButtonPressed.GetComponent<Image>().color = Color.white;
        }
        m_eventSystem.enabled = true;
        NextQuestion();
    }

    IEnumerator QuestionTime()
    {
        m_timer = m_timeForQuestion;

        for(int i=0; i < m_timeForQuestion; i++)
        {
            m_timer -= 1;
            string minutes = Mathf.Floor(m_timer / 60).ToString("00");
            string seconds = (m_timer % 60).ToString("00");
            m_timerTxt.text = string.Format("{0}:{1}", minutes, seconds);
            yield return new WaitForSecondsRealtime(1);
        }

        EndOfTime();
    }


}
