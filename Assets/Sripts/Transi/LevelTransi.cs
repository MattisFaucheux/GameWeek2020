using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransi : MonoBehaviour
{
    public static float m_level =0;
    public static float m_balloonsNeed;

    public TMPro.TextMeshProUGUI m_ballonsNeedTxt;


    // Start is called before the first frame update
    void Start()
    {
        m_level += 1;

        if (m_level == 1)
        {
            m_balloonsNeed = 3;
        }
        else if(m_level == 2)
        {
            m_balloonsNeed = 4;
        }
    }

}
