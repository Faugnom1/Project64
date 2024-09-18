using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class TextManager
{
    private static JObject _textData;

    private static void LoadTextData()
    {
        TextAsset textFile = Resources.Load<TextAsset>("TextData");

        if (textFile != null)
        {
            _textData = JObject.Parse(textFile.text);
        }
    }

    public static object GetText(string key, string language = "en")
    {
        // Load the data if first time
        if (_textData == null)
        {
            LoadTextData();
        }

        // Get the value and convert to correct type
        JToken value = _textData[language][key];

        if (value.Type == JTokenType.Array)
        {
            return value.ToObject<List<string>>();
        }
        else if (value.Type == JTokenType.Object)
        {
            return value.ToObject<Dictionary<string, string>>();
        }

        return value.ToString();
    }
}
