using UnityEngine;
using Newtonsoft.Json;

// Custom macros
using TextDictionary = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>;

public class TextManager : MonoBehaviour
{
    private static TextDictionary _textData;

    private static void LoadTextData()
    {
        TextAsset textFile = Resources.Load<TextAsset>("TextData");

        if (textFile != null)
        {
            _textData = JsonConvert.DeserializeObject<TextDictionary>(textFile.text);
        }
    }

    public static string GetText(string key, string language = "en")
    {
        if (_textData == null || _textData.Count == 0)
        {
            LoadTextData();
        }

        return _textData[language][key];
    }
}
