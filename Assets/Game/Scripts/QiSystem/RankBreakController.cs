using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(PlaySoundsComponent))]
public class RankBreakController : MonoBehaviour
{
    [SerializeField] private QiOrbController QiOrb;  
    [SerializeField] private TextMeshProUGUI ShootLabel;
    [SerializeField] private TextMeshProUGUI FilledNodesLabel;
    [SerializeField] private List<RankNode> Nodes = new List<RankNode>();
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private GameObject MouseIcon;
    [SerializeField] private Image QiFluid;

    private int nextRank = 1;
    private int maxFilledNodes = 0;
    private CharacterData master;
    private CharacterData lastMaster;
    private PlaySoundsComponent audioPlayer;

    private void Awake()
    {
        audioPlayer = GetComponent<PlaySoundsComponent>();
    }
    void OnEnable()
    {
        Cursor.visible = false;
        master = GameCore.Instance.Master;
        lastMaster = master;
        UpdateNodes();
        UpdateUI();
    }
    private void OnDisable()
    {
        Cursor.visible = true;
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
        if (filledNodes > maxFilledNodes)
        {
            maxFilledNodes = filledNodes;
            audioPlayer.Play();
        }
        FilledNodesLabel?.SetText($"Заполнено узлов: {filledNodes} / {nextRank}");

        if (filledNodes == nextRank && nextRank > 0)
        {
            master.UpdateRank();
            ExitToRankMenu();
            return;
        }
        if (master.Qi < QiOrb.QiAmount && QiOrb.OnDantian) ExitToRankMenu();
    }
    private void UpdateUI()
    {
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        QiFluid.fillAmount = (float)master.Qi / master.MaxQi;
        ShootLabel?.SetText(master.Qi >= QiOrb.QiAmount ? "Нажмите    для броска" : "Недостаточно ци для броска");
        MouseIcon?.SetActive(GameCore.Instance.Master.Qi >= QiOrb.QiAmount);
    }
    private void UpdateNodes()
    {
        maxFilledNodes = 0;
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