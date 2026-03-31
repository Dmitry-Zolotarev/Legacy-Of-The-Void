using UnityEngine;
using System.IO;

public static class SaveManager
{
    private static string Path => Application.persistentDataPath + "/save.json";
    public static bool NeedLoad = false;

    public static bool SaveExists() 
    {
        return File.Exists(Path);
    }

    public static void Save(GameCore game)
    {
        SaveData data = new SaveData();

        data.Year = game.Year;

        var master = game.Master;

        data.Age = master.Age;
        data.Name = master.Name;
        data.Generation = master.Generation;
        data.LifeLimit = master.LifeLimit;

        data.Body = master.Body;
        data.MaxBody = master.MaxBody;
        data.Qi = master.Qi;
        data.Silver = master.Silver;

        data.OpenedMeridians = master.OpenedMeridians;
        data.CurrentRank = master.CurrentRank;

        data.StartComicShown = game.StartComicShown;
        data.DemonStates.Clear();
        foreach (var demon in game.Enemies)
        {
            data.DemonStates.Add(demon.IsDead);
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Path, json);
        Debug.Log("Game Saved to " + Path);
    }

    public static void Load(GameCore game)
    {
        NeedLoad = false;

        if (!SaveExists())
        {
            Debug.LogWarning("Save file not found");
            return;
        }

        string json = File.ReadAllText(Path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        game.Year = data.Year;
        game.StartComicShown = data.StartComicShown;
        var master = game.Master;

        master.Age = data.Age;
        master.Name = data.Name;
        master.Generation = data.Generation;
        master.MaxBody = data.MaxBody;
        master.Body = data.Body;
        master.Qi = data.Qi;
        master.Silver = data.Silver;

        master.OpenedMeridians = data.OpenedMeridians;
        master.CurrentRank = data.CurrentRank;

        for (int i = 0; i < data.DemonStates.Count; i++)
        {
            if (i < game.Enemies.Count)
            {
                game.Enemies[i].IsDead = data.DemonStates[i];
            }
        }

        Debug.Log("Game Loaded");
    }
    public static void DeleteSave()
    {
        if (File.Exists(Path))
        {
            File.Delete(Path);
            Debug.Log("Save deleted");
        }
        else Debug.LogWarning("No save file to delete");
    }
}