using UnityEngine;

public class TrainingSystem : MonoBehaviour
{
    public void TrainBody(int amount)
    {
        var master = GameCore.Instance.CurrentMaster;

        if (master.healthState == CharacterStates.Normal)
        {
            master.Body += amount;
            GameCore.Instance.AdvanceTime(amount);
        }
    }
}
