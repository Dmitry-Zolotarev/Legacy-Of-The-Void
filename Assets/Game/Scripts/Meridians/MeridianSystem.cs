using System.Collections.Generic;
using UnityEngine;

public class MeridianSystem : MonoBehaviour
{
    public static MeridianSystem Instance;

    public List<Meridian> meridians = new List<Meridian>();
    [SerializeField] private float scoreToOpenMeridian = 2;
    private GameCore gameCore;

    void Awake()
    {
        if (Instance == null) Instance = this;
        gameCore = GameObject.FindGameObjectWithTag("GameCore").GetComponent<GameCore>();
    }
    public void CheckBreakthrough(float meditationScore, int meridianIndex)
    {
        if (meditationScore > scoreToOpenMeridian) OpenMeridian(meridianIndex);
    }
    public void OpenMeridian(int i)
    {
        if (gameCore == null || i >= meridians.Count) return;
        gameCore.Run.CurrentMaster.Body += meridians[i].bodyBonus;
        gameCore.Run.CurrentMaster.Spirit += meridians[i].spiritBonus;
        gameCore.Run.CurrentMaster.Body += meridians[i].qiBonus;
        meridians[i].opened = true;
    }
}