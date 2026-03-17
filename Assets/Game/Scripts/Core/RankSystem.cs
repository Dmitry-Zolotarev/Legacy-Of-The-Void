
using UnityEngine;

public class RankSystem:MonoBehaviour
{
    public bool TryRankUp()
    {
        var m=GameCore.Instance.Run.CurrentMaster;
        int power=m.Body+m.Qi+m.Spirit+m.OpenedMeridians.Count*2;
        if(power>(m.Rank+1)*20)
        {
            m.Rank+=1;
            return true;
        }
        return false;
    }
}
