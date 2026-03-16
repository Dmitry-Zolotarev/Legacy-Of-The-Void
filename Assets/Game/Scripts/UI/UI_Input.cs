using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Input : MonoBehaviour
{
    private ScreenManager screenManager;
    private PauseComponent pauseComponent;
    private void Awake()
    {
        screenManager = GetComponentInChildren<ScreenManager>();
        pauseComponent = GetComponent<PauseComponent>();
    }
    public void ToggleEscape(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            if (screenManager != null && screenManager.AnyScreenOpened())
            {
                screenManager.CloseMenus();
            }
            else pauseComponent?.Pause();
        } 
    }
}
