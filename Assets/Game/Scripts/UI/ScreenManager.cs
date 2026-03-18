using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenManager : MonoBehaviour
{
    public GameObject[] Menus;
    public void OpenMenu(int menuID)
    {
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
        //if (MeditationSystem.Instance.IsMeditating) return;
        foreach (var menu in Menus) menu?.SetActive(false);
    }
    public void CloseMenus(InputAction.CallbackContext context)
    {
        if (context.performed) CloseMenus();
    }
}
