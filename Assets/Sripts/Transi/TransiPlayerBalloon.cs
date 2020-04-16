using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransiPlayerBalloon : MonoBehaviour
{
    public GameObject[] m_balloonsList;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i< Player.m_numberBalloons; i++)
        {
            m_balloonsList[i].SetActive(true);
        }
    }
}
