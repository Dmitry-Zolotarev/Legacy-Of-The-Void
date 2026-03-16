
using UnityEngine;

public class CombatSystem:MonoBehaviour
{
    public bool Resolve(int enemyPower)
    {
        var m=GameCore.Instance.Run.CurrentMasterData;
        int power=m.Body+m.Qi+m.Spirit+m.RankId*5+m.OpenedMeridians.Count;
        bool win=power>=enemyPower;
        if(win)
        {
            m.Silver+=10;
            m.Trophies+=1;
        }
        else m.CurrentQi = Mathf.Max(0, m.CurrentQi - 5);
        m.Age+=1;
        return win;
    }
}
