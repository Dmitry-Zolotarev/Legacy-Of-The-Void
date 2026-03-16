
using UnityEngine;

public class FinalVoidBreakSystem:MonoBehaviour
{
    public bool TryFinalBreak()
    {
        var run=GameCore.Instance.Run;
        var m=run.CurrentMasterData;

        if(m.RankId>=5 && m.OpenedMeridians.Count>=12)
        {
            run.VictoryFlag=true;
            run.RunState="Victory";
            return true;
        }
        return false;
    }
}
