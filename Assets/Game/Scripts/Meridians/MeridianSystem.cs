using System.Collections.Generic;
using UnityEngine;

public class MeridianSystem : MonoBehaviour
{
    public static MeridianSystem Instance;

    public List<Meridian> meridians = new List<Meridian>();
    [SerializeField] private float scoreToOpenMeridian = 2;
    private GameCore gameCore;
    int currentIndex = 0;

    void Awake()
    {
        Instance = this;
        gameCore = GameObject.FindGameObjectWithTag("GameCore").GetComponent<GameCore>();
    }

    public void CheckBreakthrough(float meditationScore)
    {
        if (meditationScore > scoreToOpenMeridian) OpenNextMeridian();
    }
    void OpenNextMeridian()
    {
        if (gameCore == null || currentIndex >= meridians.Count) return;
        gameCore.Run.CurrentMaster.Body += meridians[currentIndex].bodyBonus;
        gameCore.Run.CurrentMaster.Body += meridians[currentIndex].qiBonus;
        currentIndex++;
    }
}