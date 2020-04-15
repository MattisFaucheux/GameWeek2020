using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class Question
{
    [XmlAttribute("name")] public string Name;
    [XmlAttribute("answer1")] public string Answer1;
    [XmlAttribute("answer2")] public string Answer2;
    [XmlAttribute("answer3")] public string Answer3;
    [XmlAttribute("answer4")] public string Answer4;
    [XmlAttribute("answer")] public string AnswerText;
}
