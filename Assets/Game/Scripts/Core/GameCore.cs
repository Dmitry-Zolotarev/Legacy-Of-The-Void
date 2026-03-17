
using UnityEngine;

public class GameCore : MonoBehaviour
{
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
    public void AdvanceTime()
    {
        var master = Instance.Run.CurrentMaster;
        master.Age += 1;
        if (master.Age >= master.LifeLimit) master.currentState = CharacterData.States.Dying;
    }
}
