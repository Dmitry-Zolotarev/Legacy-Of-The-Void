using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TravelPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Header;
    [SerializeField] private Image Icon;
    [SerializeField] private TextMeshProUGUI Effects;
    [SerializeField] private TextMeshProUGUI Rewards;
    
    [HideInInspector] public TravelData Data;

    public void UpdateContent()
    {
        if (Data == null) return;

        Header.SetText(Data.HeaderText);
        Icon.sprite = Data.Icon;
    }
}
