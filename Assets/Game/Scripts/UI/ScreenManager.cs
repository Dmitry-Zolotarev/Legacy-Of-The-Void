using UnityEngine;
public enum Canvases
{
    MeridiansCanvas,
    MeditationCanvas,
    StatsPanel,
    MapCanvas,
    GymCanvas,
    ShopCanvas,
    RankCanvas,
    RankBreakCanvas,
    StudentCanvas,
    StudentMeridiansCanvas,
    TechniquesWindow
}

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject MainHubCanvas;
    public GameObject[] Menus;
    public static ScreenManager Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void OpenMenu(int menuID)
    {
        var master = GameCore.Instance.Master;
        if (Menus[menuID].tag == "MeditationScreen" && master.Qi >= master.MaxQi) return;
        if (Menus[menuID].tag == "MeridianScreen" && master.OpenedMeridians >= 12) return;
        for (int i = 0; i < Menus.Length; i++) Menus[i]?.SetActive(i == menuID);
    }
    public void CloseMenus()
    {
        foreach (var menu in Menus) menu?.SetActive(false);
        MainHubUI.Instance.RefreshUI();
    }
}
