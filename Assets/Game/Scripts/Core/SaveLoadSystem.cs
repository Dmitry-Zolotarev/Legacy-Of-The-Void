
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public void Save()
    {
        string json = JsonUtility.ToJson(GameCore.Instance.CurrentMaster);
        PlayerPrefs.SetString("save_run", json);
    }
    public void Load()
    {
        if (PlayerPrefs.HasKey("save_run"))
        {
            string json = PlayerPrefs.GetString("save_run");
            GameCore.Instance.CurrentMaster = JsonUtility.FromJson<CharacterData>(json);
        }
    }
}
