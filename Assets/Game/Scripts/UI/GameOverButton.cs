using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverButton : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    private void OnEnable()
    {
        if (!MainHubUI.Instance.gameObject.activeSelf) SaveManager.DeleteSave();
    }
    public void GameOver()
    {
        if (MainHubUI.Instance.gameObject.activeSelf)
        {
            gameOverCanvas?.SetActive(false);
        }
        else SceneManager.LoadScene(0);
    }
}
