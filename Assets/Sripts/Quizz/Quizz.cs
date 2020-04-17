using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

[XmlRoot("Questions")]
public class Quizz
{
    [XmlArray("Quizz"), XmlArrayItem("Question")]
    public List<Question> Questions;

    //private Quizz(){}

    public static Quizz LoadFromFile(string filepath)
    {
        string path = Path.Combine(Application.dataPath, filepath);

        XmlSerializer serializer = new XmlSerializer(typeof(Quizz));
        XmlReader test = XmlReader.Create(path);
        return serializer.Deserialize(test) as Quizz;
    }
}
