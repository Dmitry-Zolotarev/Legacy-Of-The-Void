using UnityEngine;
using TMPro;

public class FinalVoidBreak : MonoBehaviour
{
    [SerializeField] private Sprite finalVoidBreakComics;
    [SerializeField] private Sprite finalVoidBreakBackround;
    [SerializeField] private TextMeshProUGUI chonMaKilledLabel;
    void OnEnable()
    {
        if (GameCore.Instance.Enemies[(int)Demons.ChonMa].IsDead)
        {
            chonMaKilledLabel.SetText("Чхон Ма убит");
            GameCore.Instance.ComicsCanvas.SetActive(true);
            ComicsBook.Instance.Image.sprite = finalVoidBreakComics;
            ComicsBook.Instance.BackgroundImage.sprite = finalVoidBreakBackround;
            ComicsBook.Instance.BackgroundImage.color = Color.white;
        }
        else chonMaKilledLabel.SetText("Чхон Ма не убит");

        SaveManager.DeleteSave();
    }

}
