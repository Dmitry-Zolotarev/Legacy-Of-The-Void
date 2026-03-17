
using UnityEngine;

public class MeditationSystem : MonoBehaviour
{
    public float FlowSpeed;
    public float Target=1f;
    public bool IsMeditating;
    public void Accelerate()
    {
        FlowSpeed += Random.Range(0.05f, 0.2f);
    }

    public void Decelerate()
    {
        FlowSpeed -= Random.Range(0.05f, 0.2f);
    }
    public void StartMeditationSession()
    {
        if (IsMeditating) return;

        var m = GameCore.Instance.Run.CurrentMaster;

        // базова€ проверка
        if (m.Qi < m.MaxQi) 
        {
            Debug.Log("Ќедостаточно ÷и дл€ открыти€ меридиана");
            return;
        }                  
        IsMeditating = true;
        // стартовые значени€
        FlowSpeed = 0f;
        Target = Random.Range(0.5f, 1.5f);
    }
    public void StopMeditationSession()
    {
        if (!IsMeditating) return;

        CompleteSession();
        IsMeditating = false;
    }
    public void CompleteSession()
    {
        var m = GameCore.Instance.Run.CurrentMaster;
        float diff=Mathf.Abs(Target - FlowSpeed);
        int gain=Mathf.Max(1,(int)(10-diff*10));
        m.Qi+=gain;
        m.MeditationProgress+=gain;
        if(m.MeditationProgress>50)m.BreakthroughReadyFlag=true;
    }
}
