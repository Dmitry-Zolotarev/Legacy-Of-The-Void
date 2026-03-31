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
        if (comicsPages.Count > 0) Image.sprite = comicsPages[0];
        BackgroundImage = GetComponent<Image>();
    }
    private void Start()
    {
        MusicPlayer.Instance.PlayStartMusic();
        if (GameCore.Instance.StartComicShown) currentPage = comicsPages.Count;
        previousButton.SetActive(currentPage > 0 && comicsPages.Contains(Image.sprite));

    }
    private void FixedUpdate()
    {
        previousButton.SetActive(currentPage > 0 && comicsPages.Contains(Image.sprite));
    }
    public void NextPage()
    {
        if (currentPage < comicsPages.Count - 1)
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
        if (currentPage > 0)
        {
            currentPage--;
            Image.sprite = comicsPages[currentPage];
        }
    }
}
