using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class MeridiansUI : MonoBehaviour
{
    [SerializeField] private RectTransform StableWay;
    [SerializeField] private RectTransform RiskyWay;
    [SerializeField] private List<Image> MeridianOrbs;
    public static MeridiansUI Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void UpdateUI()
    {
        var master = GameCore.Instance.Run.CurrentMaster;
        Debug.Log(master.CurrentMeridian.ToString());
        for(int i = 0; i < MeridianOrbs.Count; i++)
        {
            if (i < master.CurrentMeridian)
            {
                MeridianOrbs[i].color = Color.yellow;
            }            
            else MeridianOrbs[i].color = new Color(255, 255, 255, 64);
        }
        StatsPanel.Instance.UpdateLabels();
    }
    public void StartStableBreakthrough()
    {
        var master = GameCore.Instance.Run.CurrentMaster;
        if (master.Qi < master.MaxQi) return;
        master.Qi = 0;
        MeditationController.Instance.Mode = MeditationMode.StableBreakthrough;
        ScreenManager.Instance.OpenMenu(1);
        MeditationUI.Instance.ToggleSession();
    }
    public void StartRiskyBreakthrough()
    {
        var master = GameCore.Instance.Run.CurrentMaster;
        if (master.Qi < master.MaxQi / 2) return;
        master.Qi -= master.MaxQi / 2;
        MeditationController.Instance.Mode = MeditationMode.RiskyBreakthrough;
        ScreenManager.Instance.OpenMenu(1);
        MeditationUI.Instance.ToggleSession();
    }
}