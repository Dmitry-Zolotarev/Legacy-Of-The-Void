using TMPro;
using UnityEngine;
using System.Collections.Generic;
public class RankBreakController : MonoBehaviour
{
    [SerializeField] private QiOrbController QiOrb;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI ShootLabel;
    [SerializeField] private TextMeshProUGUI FilledNodesLabel;
    [SerializeField] private List<RankNode> Nodes = new List<RankNode>();
    private CharacterData master;
    private int nextRank = 1;
    void Start()
    {
        master = GameCore.Instance.CurrentMaster;
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        UpdateNodes();
        QiOrb.StartMoving();
        
    }
    private void ExitToRankMenu() 
    {
        UpdateNodes();
        ScreenManager.Instance.OpenMenu(6);
    } 
    private void FixedUpdate()
    {   
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");       
        if (master.Qi > 0) ShootLabel.SetText("Нажмите F для броска");
        else ShootLabel.SetText("Недостаточно ци для броска");

        int filledNodes = 0;
        for (int i = 0; i < nextRank; i++) 
        {
            if (Nodes[i].IsFilled) filledNodes++;
        }
        FilledNodesLabel?.SetText($"Заполнено узлов: {filledNodes} / {nextRank}");

        if (filledNodes == nextRank)
        {
            master.UpdateRank();          
            ExitToRankMenu();
        }
        else if (master.Qi <= 0) ExitToRankMenu();
        
    }
    private void UpdateNodes()
    {
        nextRank = master.CurrentRank + 1;
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].ClearNode();
            Nodes[i].gameObject.SetActive(i < nextRank);
        }
    }
}
