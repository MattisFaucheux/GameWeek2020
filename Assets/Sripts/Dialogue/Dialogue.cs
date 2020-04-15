using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string m_name;

    [TextArea(3, 10)]
    public string[] sentences;
}
