using UnityEngine;
using TMPro;
public class MeridiansUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI BreakthroughAttemptsLabel;
    public void UpdateLabels()
    {
        var master = GameCore.Instance.Run.CurrentMaster;
        BreakthroughAttemptsLabel.SetText("Попыток прорыва: " + master.BreakthroughAttempts.ToString());
    }
}
