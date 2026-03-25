using TMPro;
using UnityEngine;

public class TrainingSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI BodyElixirsLabel;
    [SerializeField] private TextMeshProUGUI QiElixirsLabel;
    [SerializeField] private int StartBodyBonus = 1;
    [SerializeField] private int ElixirPower = 2;
    private int BodyBonus = 1;
    private void OnEnable()
    {
        UpdateLabels();
    }
    private void UpdateLabels()
    {
        BodyElixirsLabel?.SetText(GameCore.Instance.Master.BodyElixirs.ToString());
        QiElixirsLabel?.SetText(GameCore.Instance.Master.QiElixirs.ToString());
    }
    public void TrainBody()
    {
        BodyBonus = StartBodyBonus;

        if (GameCore.Instance.Master.BodyElixirs > 0)
        {
            BodyBonus = StartBodyBonus * ElixirPower;
            GameCore.Instance.Master.BodyElixirs--;
        }
        GameCore.Instance.Master.Body += BodyBonus;
        GameCore.Instance.AdvanceTime(1);
        UpdateLabels();
    }
}
