
using UnityEngine;

public class GameCore:MonoBehaviour
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
        Run=new RunData();
        Run.RunId=System.Guid.NewGuid().ToString();
        Run.GenerationIndex=1;
        Run.RunState="HubActive";
        Run.CurrentMasterData=CreateMaster(1);
    }

    CharacterData CreateMaster(int generation)
    {
        CharacterData m = new CharacterData();
        m.ID = System.Guid.NewGuid().GetHashCode();
        m.Generation=generation;
        m.Age=18;
        m.LifeLimit=80;
        m.Body=1;
        m.Qi=1;
        m.Spirit=1;
        m.MaxQi=10;
        m.CurrentQi=10;
        return m;
    }
}
