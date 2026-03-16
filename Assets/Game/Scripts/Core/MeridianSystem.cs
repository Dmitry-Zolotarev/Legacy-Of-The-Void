
using UnityEngine;

public class MeridianSystem:MonoBehaviour
{
    public int TotalMeridians=12;

    public bool CanBreakthrough()
    {
        var m=GameCore.Instance.Run.CurrentMasterData;
        return m.BreakthroughReadyFlag;
    }

    public void Breakthrough()
    {
        var m=GameCore.Instance.Run.CurrentMasterData;
        int next=m.OpenedMeridians.Count;
        if(next<TotalMeridians)
        {
            m.OpenedMeridians.Add(next);
            m.BreakthroughReadyFlag=false;
        }
    }
}
