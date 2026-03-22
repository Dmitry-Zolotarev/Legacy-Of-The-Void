using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Input : MonoBehaviour
{
    private PauseComponent pauseComponent;
    private void Awake()
    {
        pauseComponent = GetComponent<PauseComponent>();
    }
    public void ToggleEscape(InputAction.CallbackContext context)
    {
        if (context.performed) pauseComponent?.Pause();
    }
}
