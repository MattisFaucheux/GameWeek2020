using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransiMenu : MonoBehaviour
{
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToNextLevel(GameObject button)
    {
        if (LevelTransi.m_balloonsNeed <= Player.m_numberBalloons)
        {
            if(LevelTransi.m_level == 1)
            {
                SceneManager.LoadScene("Canada");
            }
            else if(LevelTransi.m_level == 2)
            {
                SceneManager.LoadScene("Japon");
            }
        }
        else
        {
            button.GetComponent<Image>().color = Color.red;
        }
    }
}
