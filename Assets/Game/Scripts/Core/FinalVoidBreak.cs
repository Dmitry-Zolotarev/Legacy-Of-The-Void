using UnityEngine;
using TMPro;
using System;

public class FinalVoidBreak : MonoBehaviour
{
    [SerializeField] private Sprite finalVoidBreakComics;
    [SerializeField] private Sprite finalVoidBreakBackround;
    [SerializeField] private TextMeshProUGUI timeSpentLabel;
    void Start()
    {
        GameCore.Instance.ComicsCanvas.SetActive(true);
        ComicsBook.Instance.Image.sprite = finalVoidBreakComics;
        ComicsBook.Instance.BackgroundImage.sprite = finalVoidBreakBackround;
        ComicsBook.Instance.BackgroundImage.color = Color.white;
        timeSpentLabel.SetText($"Время игры: {TimeSpan.FromSeconds(GameCore.Instance.PlayTime):h\\:mm\\:ss}");
        SaveManager.DeleteSave();
    }
}