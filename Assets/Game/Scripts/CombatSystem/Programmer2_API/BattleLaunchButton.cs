using UnityEngine;

public class BattleLaunchButton : MonoBehaviour
{
    [SerializeField] private AutoBattleController autoBattleController;

    public BattleLaunchData BattleData;

    public void Launch()
    {
        GameCore.Instance.CombatSystem.SetActive(true);
        MusicPlayer.Instance.PlayCombatMusic();
        if (autoBattleController != null) autoBattleController.SetupExternalBattle(BattleData);
        GameCore.Instance.MainHub.SetActive(false);
        TravelSystem.Instance.gameObject.SetActive(false);
    }

}
