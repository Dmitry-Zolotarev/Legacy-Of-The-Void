using UnityEngine;

public class MeditationUI : MonoBehaviour
{
    public void StartMeditation()
    {
        MeditationManager.Instance.StartMeditation();
    }
}