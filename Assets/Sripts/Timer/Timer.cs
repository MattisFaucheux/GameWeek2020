using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject m_timerObj;

    public TMPro.TextMeshProUGUI m_timerTxt;
    public float m_initialTimer;
    private float m_timer;

    public bool m_isFinished = false;

    public AudioSource m_musicAudioSource;
    public AudioClip m_music;

    // Start is called before the first frame update
    void Start()
    {
        m_musicAudioSource.clip = m_music;
        m_musicAudioSource.loop = true;
        m_musicAudioSource.Play();
        m_timer = m_initialTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isFinished)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        m_timer -= Time.deltaTime;
        if (m_timer < 0)
        {
            EndOfTimer();
            m_timer = 0;
        }
        string minutes = Mathf.Floor(m_timer / 60).ToString("00");
        string seconds = (m_timer % 60).ToString("00");
        m_timerTxt.text = string.Format("{0}:{1}", minutes, seconds);
    }

    private void EndOfTimer()
    {
        m_musicAudioSource.Stop();
        m_isFinished = true;
        GetComponent<QuizzManager>().ActivateQuizz();
        m_timerObj.SetActive(false);
    }
}
