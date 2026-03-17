
using UnityEngine;

public class CombatSystem:MonoBehaviour
{
    public bool Resolve(int enemyPower)
    {
        var m=GameCore.Instance.Run.CurrentMaster;
        int power=m.Body+m.Qi+m.Spirit+m.Rank*5+m.OpenedMeridians.Count;
        bool win=power>=enemyPower;
        if(win)
        {
            m.Silver+=10;
            m.Trophies+=1;
        }
        else m.Qi = Mathf.Max(0, m.Qi - 5);
        m.Age+=1;
        return win;
    }
}
