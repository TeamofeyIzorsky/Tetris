using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameData currentGameData;


    private void Update()
    {
        currentGameData.allTime += Time.deltaTime;
    }

    private void SerializationSave()
    {
        Debug.Log($"SAVING");

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto
        };

        string json = JsonConvert.SerializeObject(currentGameData, settings);

        string savePath = Path.Combine(Application.persistentDataPath, $"GameData.json");

        File.WriteAllText(savePath, json);

        Debug.Log($"SAVED: " + savePath);
    }


    public void DeserializeSave()
    {
        Debug.Log($"GAME DATA LOADING");

        string savePath = Path.Combine(Application.persistentDataPath, $"GameData.json");


        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto
            };

            GameData gameData = JsonConvert.DeserializeObject<GameData>(json, settings);

            currentGameData = gameData;
        }
        else
        {
            currentGameData = new GameData();
        }
    }

    private void OnAplicationPause(bool focus)
    {
        if(!focus) {
        SerializationSave();
        }
    }

    private void OnApplicationQuit()
    {
        SerializationSave();
    }
}
