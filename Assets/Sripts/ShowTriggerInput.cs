using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTriggerInput : MonoBehaviour
{
    public GameObject m_inputObj;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            m_inputObj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_inputObj.SetActive(false);
        }
    }
}
