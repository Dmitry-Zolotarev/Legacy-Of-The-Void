using UnityEngine;

public class BattleLaunchButton : MonoBehaviour
{
    private GameObject hubRoot;
    private GameObject battleRoot;
    private AutoBattleController autoBattleController;

    public BattleLaunchData BattleData = new BattleLaunchData();
    private void Start()
    {
        hubRoot = GameCore.Instance.MainHub;
        battleRoot = GameCore.Instance.CombatSystem;
        autoBattleController = AutoBattleController.Instance;
    }
    public void Launch()
    {
        if (battleRoot != null) battleRoot.SetActive(true);
        MusicPlayer.Instance.PlayCombatMusic();
        if (autoBattleController != null) autoBattleController.SetupExternalBattle(BattleData);
        if (hubRoot != null) hubRoot.SetActive(false);       
    }

}
