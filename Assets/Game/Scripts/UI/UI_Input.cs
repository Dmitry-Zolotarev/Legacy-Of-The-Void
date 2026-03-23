using UnityEngine;

[RequireComponent(typeof(PauseComponent))]
public class UI_Input : MonoBehaviour
{
    private PauseComponent pauseComponent;

    private void Awake()
    {
        pauseComponent = GetComponent<PauseComponent>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseComponent?.Pause();
            Debug.Log("ESC Pressed");
        }
    }
}