using UnityEngine;
using UnityEngine.InputSystem;

public class PauseComponent : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private GameObject ConfirmExitMenu;
    void Start()
    {
        if (PauseMenu != null) PauseMenu.SetActive(false);
    }
    public void Pause()
    {
        if (PauseMenu == null) return;
        ConfirmExitMenu?.SetActive(false);
        OptionsMenu?.SetActive(false);
        if (PauseMenu.activeSelf && Time.timeScale == 0f)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;  
        }
        else if(Time.timeScale == 1f)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        Debug.Log("Pause Pressed " + PauseMenu);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }
}
