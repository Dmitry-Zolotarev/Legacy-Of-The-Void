
using UnityEngine;

public class MeditationSystem:MonoBehaviour
{
    public float Flow;
    public float Target=1f;

    public void Accelerate()
    {
        Flow+=0.1f;
    }

    public void Decelerate()
    {
        Flow-=0.1f;
    }

    public void CompleteSession()
    {
        var m=GameCore.Instance.Run.CurrentMasterData;
        float diff=Mathf.Abs(Target-Flow);
        int gain=Mathf.Max(1,(int)(10-diff*10));
        m.CurrentQi+=gain;
        m.MeditationProgress+=gain;
        if(m.MeditationProgress>50)m.BreakthroughReadyFlag=true;
    }
}
