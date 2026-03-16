
using UnityEngine;

public class RankSystem:MonoBehaviour
{
    public bool TryRankUp()
    {
        var m=GameCore.Instance.Run.CurrentMasterData;
        int power=m.Body+m.Qi+m.Spirit+m.OpenedMeridians.Count*2;
        if(power>(m.RankId+1)*20)
        {
            m.RankId+=1;
            return true;
        }
        return false;
    }
}
