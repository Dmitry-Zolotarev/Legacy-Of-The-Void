using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MeridianBreakController : MonoBehaviour
{
    [SerializeField] private QiOrbController QiOrb;
    [SerializeField] private TextMeshProUGUI ShootLabel;
    [SerializeField] private TextMeshProUGUI OpenedMeridiansLabel;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private Image QiFluid;   

    private CharacterData master;
    [SerializeField] private List<MeridianNode> Nodes = new List<MeridianNode>();
    private int NodesCount;
    private bool[] nodeStates;
    private void OnEnable()
    {
        StartSession(GameCore.Instance.Master, 12);
    }

    public void StartSession(CharacterData character, int nodesCount)
    {
        master = character;
        NodesCount = nodesCount;
        nodeStates = new bool[NodesCount];
        ResetNodes();
        UpdateNodes();
        QiOrb.StartMoving();
        UpdateUI();
    }

    private void ResetNodes()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].IsOpened = false;
            Nodes[i].gameObject.SetActive(false);
            Nodes[i].UpdateNode();
        }
    }

    private void UpdateNodes()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            bool active = i >= master.OpenedMeridians && i < NodesCount;
            Nodes[i].gameObject.SetActive(active);
            if (active) Nodes[i].IsOpened = false;
            Nodes[i].UpdateNode();
        }
    }

    private void FixedUpdate()
    {
        if (master == null) StartSession(GameCore.Instance.Master, 12);
        CheckNodes();
        UpdateUI();
    }

    private void CheckNodes()
    {
        for (int i = 0; i < NodesCount; i++)
        {
            if (Nodes[i].IsOpened && !nodeStates[i])
            {
                master.OpenMeridian();
            }
            nodeStates[i] = Nodes[i].IsOpened;
        }

        if (master.OpenedMeridians == NodesCount) Exit();
    }

    private void UpdateUI()
    {
        QiLabel?.SetText($"Ци: {GameCore.Instance.Master.Qi} / {GameCore.Instance.Master.MaxQi}");
        ShootLabel?.SetText(GameCore.Instance.Master.Qi > 0 ? "Нажмите F для броска" : "Недостаточно ци для броска");
        QiFluid.fillAmount = (float)master.Qi / master.MaxQi;
        if (master is Student)
        {
            OpenedMeridiansLabel?.SetText($"Открыто меридианов ученика: {master.OpenedMeridians} / {NodesCount}");
        }
        else
        {
            OpenedMeridiansLabel?.SetText($"Открыто меридианов: {master.OpenedMeridians} / {NodesCount}");
        }
    }

    public void Exit()
    {
        if (master is Student)
        {
            ScreenManager.Instance.OpenMenu((int)Canvases.StudentCanvas);
        }
        else
        {
            ScreenManager.Instance.OpenMenu((int)Canvases.GymCanvas);
        }
    }
}