using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class SaveManager : ISaveManager
{
    //Класс, отвечающий за сохранения данных между сессиями

    public void SerializeSave(GameData gameData)
    {
        Debug.Log($"SAVING");

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto
        };

        string json = JsonConvert.SerializeObject(gameData, settings);

        string savePath = Path.Combine(Application.persistentDataPath, $"GameData.json");

        File.WriteAllText(savePath, json);

        Debug.Log($"SAVED: " + savePath);
    }


    public GameData DeserializeSave()
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

            return gameData;
        }
        else
        {
            return new GameData();
        }
    }
}
