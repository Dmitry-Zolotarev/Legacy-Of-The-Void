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
        if (GameCore.Instance != null && GameCore.Instance.Master != null)
        {
            StartSession(GameCore.Instance.Master, 12);
        }
    }

    public void StartSession(CharacterData character, int nodesCount)
    {
        master = character;
        NodesCount = nodesCount;

        int count = Mathf.Min(NodesCount, Nodes.Count);
        nodeStates = new bool[count];

        ResetNodes();
        UpdateNodes();
        UpdateUI();
    }

    private void ResetNodes()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].IsOpened = false;
            Nodes[i].gameObject.SetActive(false);
        }
    }

    private void UpdateNodes()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            bool active = i < NodesCount && i >= master.OpenedMeridians;

            Nodes[i].gameObject.SetActive(active);

            if (active)
            {
                Nodes[i].IsOpened = false;
                Nodes[i].UpdateNode();
            }
        }
    }

    private void FixedUpdate()
    {
        if (master == null && GameCore.Instance != null)
        {
            StartSession(GameCore.Instance.Master, 12);
        }

        CheckNodes();
        UpdateUI();
    }

    private void CheckNodes()
    {
        int count = Mathf.Min(NodesCount, Nodes.Count);

        for (int i = 0; i < count; i++)
        {
            if (Nodes[i].IsOpened && !nodeStates[i])
            {
                master.OpenMeridian();
            }

            nodeStates[i] = Nodes[i].IsOpened;
        }

        if (master.OpenedMeridians >= NodesCount)
        {
            Exit();
        }
    }

    private void UpdateUI()
    {
        if (master == null) return;

        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");

        if (QiOrb != null)
        {
            ShootLabel?.SetText(master.Qi >= QiOrb.QiAmount
                ? "Нажмите F для броска"
                : "Недостаточно ци для броска");
        }

        if (QiFluid != null)
        {
            QiFluid.fillAmount = (float)master.Qi / master.MaxQi;
        }

        if (master != GameCore.Instance.Master)
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
        if (master != GameCore.Instance.Master)
        {
            ScreenManager.Instance.OpenMenu((int)Canvases.StudentCanvas);
        }
        else
        {
            ScreenManager.Instance.OpenMenu((int)Canvases.GymCanvas);
        }
    }
}