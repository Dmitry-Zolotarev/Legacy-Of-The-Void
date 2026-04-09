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
        data.MaxQi = master.MaxQi;

        data.Silver = master.Silver;
        data.BodyElixirs = master.BodyElixirs;
        data.QiElixirs = master.QiElixirs;     

        data.OpenedMeridians = master.OpenedMeridians;
        data.CurrentRank = master.CurrentRank;

        data.HasStudent = master.Student != null;
        
        foreach(var technique in master.KnownTechniques)
        {
            data.KnownTechniques.Add(technique.Type);
        }

        if(data.HasStudent)
        {
            data.StudentName = master.Student.Name;
            data.StudentAge = master.Student.Age;
            data.StudentLifeLimit = master.Student.LifeLimit;
            
            data.StudentQi = master.Student.Qi;
            data.StudentMaxQi = master.Student.MaxQi;
            data.StudentOpenedMeridians = master.Student.OpenedMeridians;
        }

        data.StartComicShown = game.StartComicShown;
        data.StartHelpShown = game.StartHelpShown;
        data.CombatHelpShown = game.CombatHelpShown;
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
        game.Master = new CharacterData(data);
        game.Year = data.Year;
        game.StartComicShown = data.StartComicShown;
        game.StartHelpShown = data.StartHelpShown;
        game.CombatHelpShown = data.CombatHelpShown;

        for (int i = 0; i < game.Enemies.Count; i++)
        {
            game.Enemies[i].IsDead = data.DemonStates[i];
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