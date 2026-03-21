using UnityEngine;

public class TrainingSystem : MonoBehaviour
{
    public void TrainBody()
    {
        var master = GameCore.Instance.CurrentMaster;
        
        if (master.currentState == CharacterStates.Alive)
        {
            master.Body++;
            GameCore.Instance.AdvanceTime(1);
        }
    }
}
