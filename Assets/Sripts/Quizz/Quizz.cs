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

    private Quizz(){}

    public static Quizz LoadFromFile(string filepath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Quizz));

        string p_path;
        p_path = Path.Combine(Application.dataPath, filepath);

        using (FileStream stream = new FileStream(p_path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as Quizz;
        }

    }
}



//TextAsset temp = Resources.Load(filepath) as TextAsset;
//XmlDocument _doc = new XmlDocument();
//_doc.LoadXml(temp.text);
