using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TImer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI m_timerTxt;
    public float m_initialTimer;
    private float m_timer;

    public bool m_isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        m_timer = m_initialTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isActive)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        m_timer -= Time.deltaTime;
        if (m_timer < 0)
        {
            m_timer = 0;
        }
        string minutes = Mathf.Floor(m_timer / 60).ToString("00");
        string seconds = (m_timer % 60).ToString("00");
        m_timerTxt.text = string.Format("{0}:{1}", minutes, seconds);
    }
}
