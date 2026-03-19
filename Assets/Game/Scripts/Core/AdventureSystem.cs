
using UnityEngine;

public class AdventureSystem:MonoBehaviour
{
    public void Travel(int months)
    {
        var m=GameCore.Instance.Run.CurrentMaster;
        int roll=Random.Range(0,100);
        if(roll<60)
        {
            m.Silver+=Random.Range(5,20);
            m.Trophies+=1;
        }
        else m.Qi = Mathf.Max(0, m.Qi - 3);
        m.AgeMonths += months;
    }
}
