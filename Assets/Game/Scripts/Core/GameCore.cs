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
    public void AdvanceTime(int yearsAmount)
    {   
        var master = Run.CurrentMaster;
        master.Age += yearsAmount;

        if (master.Age > Run.CurrentMaster.LifeLimit) 
        {
            master.currentState = CharacterStates.Dead;
            StatsPanel.Instance.UpdateLabels();
            return;
        }
        StatsPanel.Instance.UpdateLabels();

        if (master.Age >= 60) MasterSprite.sprite = OldMasterSprite;
        else if (master.Age >= 40) MasterSprite.sprite = AdultMasterSprite;
        else MasterSprite.sprite = YoungMasterSprite;
    }
}
