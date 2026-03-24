using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class MeridianBreakController : MonoBehaviour
{
    [SerializeField] private QiOrbController QiOrb;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI ShootLabel;
    [SerializeField] private TextMeshProUGUI OpenedMeridiansLabel;

    private CharacterData master;
    [SerializeField] private List<MeridianNode> Nodes = new List<MeridianNode>();
    private int NodesCount;

    void Start()
    {
        StartSession(master, 12);
    }

    public void StartSession(CharacterData character, int nodesCount)
    {
        
        bool differentPeople = master != character;
        master = character;
        NodesCount = nodesCount;

        if (differentPeople) UpdateNodes();   
        QiOrb.StartMoving();
        UpdateUI();
    }
    private void Update()
    {
        if (master == null) StartSession(GameCore.Instance.CurrentMaster, 12); 
    }
    private void FixedUpdate() => UpdateUI();

    private void UpdateUI()
    {
        QiLabel?.SetText($"Ци: {GameCore.Instance.CurrentMaster.Qi} / {GameCore.Instance.CurrentMaster.MaxQi}");
        ShootLabel.SetText(GameCore.Instance.CurrentMaster.Qi > 0 ? "Нажмите F для броска" : "Недостаточно ци для броска");
        if (master is Student)
        {
            OpenedMeridiansLabel?.SetText("");
        }
        else OpenedMeridiansLabel?.SetText($"Открыто меридианов: {master.OpenedMeridians} / {NodesCount}");
    }
    private void UpdateNodes()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].gameObject.SetActive(i < NodesCount);
            Nodes[i].master = master;
            Nodes[i].UpdateNode();
        }
    }
}