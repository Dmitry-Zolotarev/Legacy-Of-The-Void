
using UnityEngine;

public class MeditationSystem : MonoBehaviour
{
    public static MeditationSystem Instance;
    [SerializeField] private float FlowSpeed, Target = 1f; 
    [SerializeField] private GameObject ButtonPanel;

    public bool IsMeditating = false;
    private int MeditationProgress = 0, SelectedMeridian = -1;
    public void Accelerate()
    {
        FlowSpeed += Random.Range(0.05f, 0.2f);
    }
    public void Decelerate()
    {
        FlowSpeed -= Random.Range(0.05f, 0.2f);
    }
    public void StartMeditationSession(int selectedMeridian)
    {
        if (IsMeditating || selectedMeridian < 0) return;
        var master = GameCore.Instance.Run.CurrentMaster;

        // базовая проверка
        if (master.Qi < master.MaxQi) 
        {
            Debug.Log("Недостаточно Ци для открытия меридиана");
            return;
        }
        SelectedMeridian = selectedMeridian;
        MeditationProgress = 0;
        ButtonPanel.SetActive(false);
        IsMeditating = true;
        
        FlowSpeed = 0f;
        Target = Random.Range(0.5f, 1.5f);
    }
    public void CompleteSession()
    {
        var m = GameCore.Instance.Run.CurrentMaster;
        float diff=Mathf.Abs(Target - FlowSpeed);
        int gain=Mathf.Max(1,(int)(10-diff*10));
        m.Qi+=gain;
        MeditationProgress+=gain;
        MeridianSystem.Instance.OpenMeridian(SelectedMeridian);
        ButtonPanel.SetActive(true);
    }
}
