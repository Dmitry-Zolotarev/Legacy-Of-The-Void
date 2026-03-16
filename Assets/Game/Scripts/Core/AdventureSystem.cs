
using UnityEngine;

public class AdventureSystem:MonoBehaviour
{
    public void Travel()
    {
        var m=GameCore.Instance.Run.CurrentMasterData;
        int roll=Random.Range(0,100);
        if(roll<60)
        {
            m.Silver+=Random.Range(5,20);
            m.Trophies+=1;
        }
        else
        {
            m.CurrentQi=Mathf.Max(0,m.CurrentQi-3);
        }
        m.Age+=1;
    }
}
