using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class RankBreakController : MonoBehaviour
{
    [SerializeField] private QiOrbController QiOrb;  
    [SerializeField] private TextMeshProUGUI ShootLabel;
    [SerializeField] private TextMeshProUGUI FilledNodesLabel;
    [SerializeField] private List<RankNode> Nodes = new List<RankNode>();
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private Image QiFluid;

    private CharacterData master;
    private CharacterData lastMaster;

    private int nextRank = 1;

    void Start()
    {
        master = GameCore.Instance.Master;
        lastMaster = master;
        UpdateNodes();
        QiOrb.StartMoving();
        UpdateUI();
    }

    private void FixedUpdate()
    {
        master = GameCore.Instance.Master;
        if (master != lastMaster)
        {
            UpdateNodes();
            lastMaster = master;
        }
        UpdateUI();
        int filledNodes = 0;
        for (int i = 0; i < nextRank; i++)
        {
            if (Nodes[i].IsFilled) filledNodes++;
        }
        FilledNodesLabel?.SetText($"Заполнено узлов: {filledNodes} / {nextRank}");

        if (filledNodes == nextRank && nextRank > 0)
        {
            master.UpdateRank();
            GameCore.Instance.AdvanceTime(1);
            ExitToRankMenu();
            return;
        }
        if (master.Qi <= 0 && QiOrb.OnDantian) ExitToRankMenu();
    }
    private void UpdateUI()
    {
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        QiFluid.fillAmount = (float)master.Qi / master.MaxQi;
        ShootLabel?.SetText(master.Qi > 0 ? "Нажмите F для броска" : "Недостаточно ци для броска");
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
    private void ExitToRankMenu()
    {
        UpdateNodes();
        ScreenManager.Instance.OpenMenu((int)Canvases.RankCanvas);
    }
}