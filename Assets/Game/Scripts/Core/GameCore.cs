
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public static GameCore Instance;
    public RunData Run;
    [SerializeField] private StatsPanel statsPanel;
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
        if (master.Age >= master.LifeLimit) master.currentState = CharacterData.States.Dead;
        statsPanel?.UpdateLabels();
        master.Age++;
    }
}
