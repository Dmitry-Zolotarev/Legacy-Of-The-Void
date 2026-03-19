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
        if (Menus[menuID].tag == "MeditationScreen" && GameCore.Instance.Run.CurrentMaster.BreakthroughAttempts > 0) return;
        for (int i = 0; i < Menus.Length; i++) Menus[i]?.SetActive(i == menuID);
    }     
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
        if (MeditationController.Instance.State == MeditationState.Running)
        {
            MeditationUI.Instance.ToggleSession();
        }
        else foreach (var menu in Menus) menu?.SetActive(false);
    }
    public void CloseMenus(InputAction.CallbackContext context)
    {
        if (context.performed) CloseMenus();
    }
}
