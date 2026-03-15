using System.Collections.Generic;
using UnityEngine;

public class MeridianSystem : MonoBehaviour
{
    public static MeridianSystem Instance;

    public List<Meridian> meridians = new List<Meridian>();

    int currentIndex = 0;

    void Awake()
    {
        Instance = this;
    }

    public void CheckBreakthrough(float meditationScore)
    {
        if (meditationScore > 2)
            OpenNextMeridian();
    }

    void OpenNextMeridian()
    {
        if (currentIndex >= meridians.Count)
            return;

        Meridian m = meridians[currentIndex];

        m.opened = true;

        GameManager.Instance.Player.stats.body += m.bodyBonus;
        GameManager.Instance.Player.stats.qi += m.qiBonus;

        currentIndex++;

        Debug.Log("Opened meridian: " + m.name);
    }
}