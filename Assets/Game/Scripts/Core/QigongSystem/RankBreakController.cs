using TMPro;
using UnityEngine;
using System.Collections.Generic;
public class RankBreakController : MonoBehaviour
{
    [SerializeField] private QiOrbController QiOrb;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI ShootLabel;
    [SerializeField] private List<RankNode> RankNodes = new List<RankNode>();
    private CharacterData master;

    void Start()
    {
        master = GameCore.Instance.CurrentMaster;
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        QiOrb.StartMoving();
    }
    private void FixedUpdate()
    {
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");

        if (master.Qi > 0) ShootLabel.SetText("Нажмите F для броска");
        else ShootLabel.SetText("Недостаточно ци для броска");

        bool allNodesFilled = true;
        foreach(var node in RankNodes)
        {
            if(!node.isFilled)
            {
                allNodesFilled = false;
                break;
            }
        }
        if(allNodesFilled)
        {
            master.UpdateRank();
            ScreenManager.Instance.OpenMenu(6);
        }
    }
}
