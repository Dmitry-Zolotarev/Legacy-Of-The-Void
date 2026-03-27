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
        var master = GameCore.Instance.Master;
        BodyBonus = StartBodyBonus;
        
        if(master.Body < master.MaxBody)
        {
            if (master.BodyElixirs > 0)
            {
                BodyBonus = StartBodyBonus * ElixirPower;
                master.BodyElixirs--;
                UpdateLabels();
            }
            master.TrainBody(BodyBonus);
            GameCore.Instance.AdvanceTime(1); 
        }       
    }
}
