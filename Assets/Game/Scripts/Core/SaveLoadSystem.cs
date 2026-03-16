
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public void Save()
    {
        string json = JsonUtility.ToJson(GameCore.Instance.Run);
        PlayerPrefs.SetString("save_run", json);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("save_run"))
        {
            string json = PlayerPrefs.GetString("save_run");
            GameCore.Instance.Run = JsonUtility.FromJson<RunData>(json);
        }
    }
}
