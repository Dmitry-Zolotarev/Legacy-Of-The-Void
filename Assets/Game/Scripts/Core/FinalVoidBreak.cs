using UnityEngine;
using TMPro;

public class FinalVoidBreak : MonoBehaviour
{
    [SerializeField] private Sprite finalVoidBreakComics;
    [SerializeField] private Sprite finalVoidBreakBackround;
    void OnEnable()
    {
        GameCore.Instance.ComicsCanvas.SetActive(true);
        ComicsBook.Instance.Image.sprite = finalVoidBreakComics;
        ComicsBook.Instance.BackgroundImage.sprite = finalVoidBreakBackround;
        ComicsBook.Instance.BackgroundImage.color = Color.white;
        SaveManager.DeleteSave();
    }
}
