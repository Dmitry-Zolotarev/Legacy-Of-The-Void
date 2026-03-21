using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class MeridiansUI : MonoBehaviour
{
    [SerializeField] private RectTransform StableWay;
    [SerializeField] private RectTransform RiskyWay;
    [SerializeField] private List<Image> MeridianOrbs;
    public static MeridiansUI Instance;
    private CharacterData master;
    private Color defaultOrbColor;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start()
    {
        master = GameCore.Instance.CurrentMaster;
        if(MeridianOrbs.Count > 0) defaultOrbColor = MeridianOrbs[0].color;
        UpdateUI();
    }
    private void FixedUpdate() => UpdateUI();

    private void UpdateUI()
    {
        StableWay.gameObject.SetActive(master.Qi >= master.MaxQi);
        RiskyWay.gameObject.SetActive(master.Qi >= master.MaxQi / 2);
        for(int i = 0; i < MeridianOrbs.Count; i++)
        {
            if (i < master.OpenedMeridians)
            {
                MeridianOrbs[i].color = Color.yellow;
            }            
            else MeridianOrbs[i].color = defaultOrbColor;
        }
    }
    public void StartStableBreakthrough()
    {
        if (master.Qi < master.MaxQi) return;
        master.Qi = 0;
        MeditationController.Instance.Mode = MeditationMode.StableBreakthrough;
        ScreenManager.Instance.OpenMenu(1);
        MeditationUI.Instance.Start();
        MeditationUI.Instance.ToggleSession();
    }
    public void StartRiskyBreakthrough()
    {
        if (master.Qi < master.MaxQi / 2) return;
        master.Qi -= master.MaxQi / 2;
        MeditationController.Instance.Mode = MeditationMode.RiskyBreakthrough;
        ScreenManager.Instance.OpenMenu(1);
        MeditationUI.Instance.ToggleSession();
    }
}