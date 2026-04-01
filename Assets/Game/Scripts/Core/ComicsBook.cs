using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
[RequireComponent(typeof(Image))]
public class ComicsBook : MonoBehaviour
{  
    public Image Image;
    [HideInInspector] public Image BackgroundImage;
    [SerializeField] private GameObject comicsCanvas;
    [SerializeField] private GameObject previousButton;
    [SerializeField] private List<Sprite> comicsPages;

    private int currentPage = 0;

    public static ComicsBook Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        BackgroundImage = GetComponent<Image>();
    }
    private void OnEnable()
    {
        MusicPlayer.Instance.PlayStartMusic();
        GameCore.Instance.StartComicShown = comicsPages.Contains(Image.sprite);
        previousButton.SetActive(currentPage > 0 && GameCore.Instance.StartComicShown);
    }
    private void FixedUpdate()
    {
        previousButton.SetActive(currentPage > 0 && GameCore.Instance.StartComicShown);
    }
    public void NextPage()
    {
        if (currentPage < comicsPages.Count - 1 && GameCore.Instance.StartComicShown)
        {
            currentPage++;
            Image.sprite = comicsPages[currentPage];
        }
        else
        {
            MusicPlayer.Instance.PlayMainMusic();
            GameCore.Instance.StartComicShown = true;
            comicsCanvas.SetActive(false);         
        } 
    }
    public void PreviousPage()
    {
        if (currentPage > 0 && GameCore.Instance.StartComicShown)
        {
            currentPage--;
            Image.sprite = comicsPages[currentPage];
        }
    }
}
