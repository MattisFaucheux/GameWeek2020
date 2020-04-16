﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject m_mainMenu;
    public GameObject m_credits;
    public GameObject m_turtle;
    public EventSystem m_eventSystem;

    public GameObject m_firstButtonMM;
    public GameObject m_firstButtonC;


    void Start()
    {
        m_turtle.SetActive(true);
        m_mainMenu.SetActive(true);
        m_credits.SetActive(false);
        m_eventSystem.SetSelectedGameObject(m_firstButtonMM);
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToCredits()
    {
        m_turtle.SetActive(false);
        m_mainMenu.SetActive(false);
        m_credits.SetActive(true);
        m_eventSystem.SetSelectedGameObject(m_firstButtonC);
    }

    public void GoToMainMenu()
    {
        m_turtle.SetActive(true);
        m_mainMenu.SetActive(true);
        m_credits.SetActive(false);
        m_eventSystem.SetSelectedGameObject(m_firstButtonMM);
    }


}
