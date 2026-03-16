
using UnityEngine;

public class DiscipleSystem:MonoBehaviour
{
    public void UnlockDisciple()
    {
        var run=GameCore.Instance.Run;
        run.DiscipleData=new DiscipleData();
        run.DiscipleData.DiscipleUnlockedFlag=true;
    }

    public void InfuseQi(int amount)
    {
        var run=GameCore.Instance.Run;
        var m=run.CurrentMasterData;
        if(m.CurrentQi>=amount)
        {
            m.CurrentQi-=amount;
            run.DiscipleData.DiscipleQiReserve+=amount;
        }
    }

    public void TransferTechnique(string id)
    {
        var run=GameCore.Instance.Run;
        if(!run.DiscipleData.DiscipleLearnedTechniques.Contains(id))
            run.DiscipleData.DiscipleLearnedTechniques.Add(id);
    }

    public void CheckReady()
    {
        var d=GameCore.Instance.Run.DiscipleData;
        if(d.DiscipleQiReserve>20)d.DiscipleReadyFlag=true;
    }
}
