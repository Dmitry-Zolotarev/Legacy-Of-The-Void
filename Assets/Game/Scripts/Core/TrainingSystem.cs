using UnityEngine;

public class TrainingSystem:MonoBehaviour
{
    [SerializeField] private int BodyTrainInrease = 1, SpiritTrainInrease = 1, QiTrainIncrease = 10;
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
    public void TrainQi()
    {
        var master = GameCore.Instance.Run.CurrentMaster;
        if (master.Qi >= master.MaxQi)
        {
            master.Qi = master.MaxQi;
        } 
        else if (master.currentState == CharacterStates.Alive)
        {
            master.Qi += QiTrainIncrease;
            GameCore.Instance.AdvanceTime(1);
        }      
    }  
}
