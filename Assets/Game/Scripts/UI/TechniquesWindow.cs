using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TechniquesWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI EquippedTechniqueLabel;
    [SerializeField] private List<TechniquePanel> TechniquePanels;

    private void OnEnable()
    {
        foreach (var panel in TechniquePanels) 
        {
            bool IsKnown = GameCore.Instance.Master.KnownTechniques.Contains(panel.Technique);
            panel.gameObject.SetActive(IsKnown);
        }
        UpdateLabels();
    }
    private void FixedUpdate()
    {
        UpdateLabels();
    }
    private void UpdateLabels()
    {
        EquippedTechniqueLabel?.SetText("Выбранная техника: " + GameCore.Instance.Master.EquippedTechnique.Name.ToLower());
    }
}
