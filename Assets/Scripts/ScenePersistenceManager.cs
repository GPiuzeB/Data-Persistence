using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScenePersistenceManager : MonoBehaviour
{
    public static ScenePersistenceManager Instance;

    public int highScore;
    public string nameHS;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string nameHS;
    }


    public void saveName(string s)
    {
        nameHS = s;
        Debug.Log(nameHS);
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.nameHS = nameHS;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            nameHS = data.nameHS;
        }
    }
}
