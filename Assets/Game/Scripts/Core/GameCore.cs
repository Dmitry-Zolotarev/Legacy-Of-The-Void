using UnityEngine;
using UnityEngine.UI;
public class GameCore : MonoBehaviour
{
    [SerializeField] private StatsPanel statsPanel;
    [SerializeField] private Sprite YoungMasterSprite, AdultMasterSprite, OldMasterSprite;
    [SerializeField] private Image MasterSprite;
    public static GameCore Instance;
    public RunData Run;   
    void Awake()
    {      
        if (Instance == null) Instance = this;
        if (Run == null) StartGame();
        if (statsPanel == null) statsPanel = GameObject.FindGameObjectWithTag("StatsPanel").GetComponent<StatsPanel>();
    }
    public void StartGame()
    {
        Run = new RunData();
        Run.RunId = System.Guid.NewGuid().ToString();
        Run.GenerationIndex = 1;
        Run.RunState = "HubActive";
        Run.CurrentMaster = new CharacterData();
    }
    public void AdvanceTime()
    {
        
        var master = Run.CurrentMaster;
        master.Age++;
        if (master.Age > master.LifeLimit) 
        {
            master.currentState = CharacterStates.Dead;
            statsPanel?.UpdateLabels();
            return;
        }
        statsPanel?.UpdateLabels();

        if (master.Age >= 60) MasterSprite.sprite = OldMasterSprite;
        else if (master.Age >= 40) MasterSprite.sprite = AdultMasterSprite;
        else MasterSprite.sprite = YoungMasterSprite;
    }
}
