using UnityEngine;
public enum Canvases
{
    MeridiansCanvas,
    MeditationCanvas,
    StatsPanel,
    TravelCanvas,
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
    public GameObject MainHubCanvas;
    public GameObject[] Menus;
    public static ScreenManager Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void OpenMenu(int menuID)
    {
        Time.timeScale = 1f;
        MainHubCanvas.SetActive(false);
        var master = GameCore.Instance.Master;
        if (Menus[menuID].tag == "MeditationScreen" && master.Qi >= master.MaxQi) return;
        if (Menus[menuID].tag == "MeridianScreen" && master.OpenedMeridians >= 12) return;

        if (Menus[menuID].tag == "GymCanvas" && !GameCore.Instance.StartHelpShown)
        {
            GameCore.Instance.StartHelpCanvas.SetActive(true);
            GameCore.Instance.StartHelpShown = true;
        }
        for (int i = 0; i < Menus.Length; i++) Menus[i]?.SetActive(i == menuID);
    }
    public void CloseMenus()
    {
        MainHubCanvas.SetActive(true);
        foreach (var menu in Menus) menu?.SetActive(false);
        MainHubUI.Instance.RefreshUI();
    }
}
