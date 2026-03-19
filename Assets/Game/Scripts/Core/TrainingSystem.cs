using UnityEngine;

public class TrainingSystem : MonoBehaviour
{
    public void TrainBody()
    {
        var master = GameCore.Instance.Run.CurrentMaster;
        
        if (master.currentState == CharacterStates.Alive)
        {
            if (master.Qi > 0) master.Qi--;
            else return;

            master.Body++;
            GameCore.Instance.AdvanceTime(1);
        }
    }
}
