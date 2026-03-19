using UnityEngine;

public class TrainingSystem:MonoBehaviour
{
    [SerializeField] private int BodyTrainInrease = 1;
    [SerializeField] private int SpiritTrainInrease = 1;
    public void TrainBody()
    {
        var master = GameCore.Instance.Run.CurrentMaster;
        if (master.currentState == CharacterStates.Alive)
        {
            master.Body += BodyTrainInrease;
            GameCore.Instance.AdvanceTime(1);
        }
    }
    public void TrainSpirit()
    {
        var master = GameCore.Instance.Run.CurrentMaster;
        if(master.currentState == CharacterStates.Alive)
        {
            master.Spirit += SpiritTrainInrease;
            GameCore.Instance.AdvanceTime(1);
        }     
    } 
}
