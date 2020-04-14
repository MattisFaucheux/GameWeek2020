﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Questions")]
public class Quizz
{
    [XmlArray("Quizz"), XmlArrayItem("Question")]
    public List<Question> Questions;

    private Quizz(){}

    public static Quizz LoadFromFile(string filepath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Quizz));
        using (FileStream stream = new FileStream(filepath, FileMode.Open))
        {
            return serializer.Deserialize(stream) as Quizz;
        }

    }
}
