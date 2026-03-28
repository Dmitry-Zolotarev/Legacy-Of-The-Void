using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TechniquePanel : MonoBehaviour
{
    
    [HideInInspector] public Technique Technique;
    [SerializeField] private int TechniqueID;
    [SerializeField] private Image Icon;
    [SerializeField] private TextMeshProUGUI TechniqueName;
    [SerializeField] private TextMeshProUGUI Rank;
    void OnEnable()
    {
        Technique = GameCore.Instance.Techniques[TechniqueID];
        TechniqueName.SetText(Technique.Name);
        Rank.SetText("Ранг: " + GameCore.Instance.Master.GetRankName(Technique.RequiredRank).ToLower());
        if(Technique.Icon != null)Icon.sprite = Technique.Icon;
    }
}
