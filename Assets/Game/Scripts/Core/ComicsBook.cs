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

    private void OnEnable()
    {
        Instance = this;
        BackgroundImage = GetComponent<Image>();
        MusicPlayer.Instance.PlayStartMusic();
        previousButton.SetActive(currentPage > 0 && comicsPages.Contains(Image.sprite));
        Time.timeScale = 1f;
    }
    private void FixedUpdate()
    {
        previousButton.SetActive(currentPage > 0 && comicsPages.Contains(Image.sprite));
    }
    public void NextPage()
    {
        if (currentPage < comicsPages.Count - 1 && comicsPages.Contains(Image.sprite))
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
        if (currentPage > 0 && comicsPages.Contains(Image.sprite))
        {
            currentPage--;
            Image.sprite = comicsPages[currentPage];
        }
    }
}
