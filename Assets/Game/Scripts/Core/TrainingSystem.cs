using UnityEngine;

public class TrainingSystem : MonoBehaviour
{
    public void TrainBody(int amount)
    {
        GameCore.Instance.CurrentMaster.Body += amount;
        GameCore.Instance.AdvanceTime(amount);
    }
}
