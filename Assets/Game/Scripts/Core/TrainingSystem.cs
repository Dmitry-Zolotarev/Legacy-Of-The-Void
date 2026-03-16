using UnityEngine;

public class TrainingSystem:MonoBehaviour
{
    public void TrainBody()
    {
        var m=GameCore.Instance.Run.CurrentMasterData;
        m.Body+=1;
        AdvanceTime();
    }

    public void TrainQi()
    {
        var m=GameCore.Instance.Run.CurrentMasterData;
        m.Qi+=1;
        AdvanceTime();
    }

    public void TrainSpirit()
    {
        var m=GameCore.Instance.Run.CurrentMasterData;
        m.Spirit+=1;
        AdvanceTime();
    }

    void AdvanceTime()
    {
        var m=GameCore.Instance.Run.CurrentMasterData;
        m.Age+=1;
        if(m.Age>=m.LifeLimit)m.AliveState="Dying";
    }
}
