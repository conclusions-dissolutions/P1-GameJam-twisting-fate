using UnityEngine;

[System.Serializable]
public class DialogObject
{
    public int Id;
    public string Character;
    public string Speed;
    public string Color;
    public string Font;
    public string Line;
}

[System.Serializable]
public class Dialog
{
    public DialogObject[] FirstScene;
    public DialogObject[] SecondScene;
    public DialogObject[] ThirdScene;
}

public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile;

    void Start()
    {
        Dialog dialog = JsonUtility.FromJson<Dialog>(jsonFile.text);

        foreach (DialogObject obj in dialog.FirstScene)
        {
            Debug.Log($"Id: {obj.Id}; Character:{obj.Character}; Speed:{obj.Speed}; Color:{obj.Color}; Font:{obj.Font}; Line:{obj.Line}");
        }

        foreach (DialogObject obj in dialog.SecondScene)
        {
            Debug.Log($"Id: {obj.Id}; Character:{obj.Character}; Speed:{obj.Speed}; Color:{obj.Color}; Font:{obj.Font}; Line:{obj.Line}");
        }

        foreach (DialogObject obj in dialog.ThirdScene)
        {
            Debug.Log($"Id: {obj.Id}; Character:{obj.Character}; Speed:{obj.Speed}; Color:{obj.Color}; Font:{obj.Font}; Line:{obj.Line}");
        }
    }
}
