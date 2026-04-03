using TMPro;
using UnityEngine;

[RequireComponent(typeof(ParticleSpawner))]
public class TrainingSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI BodyElixirsLabel;
    [SerializeField] private TextMeshProUGUI BodyLabel;
    [SerializeField] private int StartBodyBonus = 1;
    [SerializeField] private int ElixirPower = 2;
    private ParticleSpawner spawner;
    private int BodyBonus = 1;

    private void Awake()
    {
        spawner = GetComponent<ParticleSpawner>();
    }
    private void OnEnable()
    {
        UpdateLabels();
    }
    private void UpdateLabels()
    {
        BodyElixirsLabel?.SetText(GameCore.Instance.Master.BodyElixirs.ToString());
    }
    public void TrainBody()
    {
        var master = GameCore.Instance.Master;
        BodyBonus = StartBodyBonus;
        
        if(master.Body < master.MaxBody)
        {
            if (master.BodyElixirs > 0)
            {
                BodyBonus *= ElixirPower;
                master.BodyElixirs--;          
                UpdateLabels();
                spawner.Spawn(BodyElixirsLabel.transform, $"-1", Color.red);
            }
            int bodyTrained = master.TrainBody(BodyBonus);
            spawner.Spawn(BodyLabel.transform, $"+{bodyTrained}", Color.green);
            
            GameCore.Instance.AdvanceTime(1); 
        }       
    }
}
