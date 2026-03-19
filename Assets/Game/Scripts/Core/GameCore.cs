using UnityEngine;
using UnityEngine.UI;

public class GameCore : MonoBehaviour
{
    [SerializeField] private Sprite YoungMasterSprite, AdultMasterSprite, OldMasterSprite;
    [SerializeField] private Image MasterSprite;
    public static GameCore Instance;
    public RunData Run;   
    void Awake()
    {      
        if (Instance == null) Instance = this;
        if (Run == null) StartGame();
    }
    public void StartGame()
    {
        Run = new RunData();
        Run.RunId = System.Guid.NewGuid().ToString();
        Run.GenerationIndex = 1;
        Run.RunState = "HubActive";
        Run.CurrentMaster = new CharacterData();
    }
    public void AdvanceTime(int months)
    {   
        var master = Run.CurrentMaster;
        master.AgeMonths += months;

        if (master.AgeMonths > Run.CurrentMaster.LifeLimit * 12) 
        {
            master.currentState = CharacterStates.Dead;
            StatsPanel.Instance.UpdateLabels();
            return;
        }
        StatsPanel.Instance.UpdateLabels();

        if (master.AgeMonths >= 60 * 12) MasterSprite.sprite = OldMasterSprite;
        else if (master.AgeMonths >= 40 * 12) MasterSprite.sprite = AdultMasterSprite;
        else MasterSprite.sprite = YoungMasterSprite;
    }
}
