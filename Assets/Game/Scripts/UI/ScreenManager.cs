using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenManager : MonoBehaviour
{
    public GameObject[] Menus;
    public static ScreenManager Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void OpenMenu(int menuID)
    {
        var master = GameCore.Instance.CurrentMaster;
        if (Menus[menuID].tag == "MeditationScreen" && master.Qi >= master.MaxQi) return;

        for (int i = 0; i < Menus.Length; i++) Menus[i]?.SetActive(i == menuID);
    }
    public void CloseMenu(int menuID) => Menus[menuID].SetActive(false);
    public bool AnyScreenOpened()
    {
        foreach (var menu in Menus)
        {
            if (menu.activeSelf) return true;
        }
        return false;
    }
    public void CloseMenus()
    {
        foreach (var menu in Menus) menu?.SetActive(false);
    }
    
    public void CloseMenus(InputAction.CallbackContext context)
    {
        if (context.performed) CloseMenus();
    }
}
