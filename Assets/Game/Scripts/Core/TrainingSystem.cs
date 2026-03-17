using UnityEngine;

public class TrainingSystem:MonoBehaviour
{
    public void TrainBody()
    {
        var m=GameCore.Instance.Run.CurrentMaster;
        m.Body+=1;
        GameCore.Instance.AdvanceTime();
    }
    public void TrainQi()
    {
        var m=GameCore.Instance.Run.CurrentMaster;
        m.Qi+=1;
        GameCore.Instance.AdvanceTime();
    }

    public void TrainSpirit()
    {
        var m=GameCore.Instance.Run.CurrentMaster;
        m.Spirit+=1;
        GameCore.Instance.AdvanceTime();
    }

}
