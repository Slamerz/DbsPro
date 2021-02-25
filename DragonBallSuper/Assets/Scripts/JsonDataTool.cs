using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonDataTool 
{ 
    public static string GetJsonFromFile(string path)
    {
        string str = File.ReadAllText(Application.dataPath + path, Encoding.GetEncoding("UTF-8"));
        if (str == null) Debug.LogError("Target resource not found:" + path);
        return str;
    }
}
