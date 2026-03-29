using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class ComicsBook : MonoBehaviour
{
    [SerializeField] private Sprite[] comicsPages;
    [SerializeField] private GameObject comicsCanvas;
    private Image image;
    private int currentPage = 0;

    void Awake()
    {
        image = GetComponent<Image>();
        if (comicsPages.Length > 0) image.sprite = comicsPages[0];
    }
    private void Start()
    {
        MusicPlayer.Instance.PlayStartMusic();
    }
    public void SwitchPage()
    {
        if (currentPage < comicsPages.Length - 1)
        {
            currentPage++;
            image.sprite = comicsPages[currentPage];
        }
        else comicsCanvas.SetActive(false);
    }
}
