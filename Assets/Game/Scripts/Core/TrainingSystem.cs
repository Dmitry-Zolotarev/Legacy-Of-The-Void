using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ParticleSpawner))]
public class TrainingSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI BodyElixirsLabel;
    [SerializeField] private TextMeshProUGUI QiElixirsLabel;
    [SerializeField] private TextMeshProUGUI BodyLabel;
    [SerializeField] private int StartBodyBonus = 1;
    [SerializeField] private int ElixirPower = 2;
    [SerializeField] private Animator animator;
    [SerializeField] private Image background;
    [SerializeField] private Sprite gymBackground;
    [SerializeField] private Sprite combatBackground;
    [SerializeField] private GameObject mainHub;
    [SerializeField] private float trainingTime = 2f;
    private ParticleSpawner spawner;
    private int BodyBonus = 1;

    private void Awake()
    {
        spawner = GetComponent<ParticleSpawner>();
    }
    private void FixedUpdate()
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
        StartCoroutine(TrainBodyCoroutine());   
    }
    private IEnumerator TrainBodyCoroutine()
    {      
        var master = GameCore.Instance.Master;
        BodyBonus = StartBodyBonus;

        if (master.Body < master.MaxBody)
        {
            mainHub.SetActive(false);
            background.sprite = gymBackground;
            animator.gameObject.SetActive(true);
            animator.Play("Push Up");
            yield return new WaitForSeconds(trainingTime);

            background.sprite = combatBackground;
            animator.gameObject.SetActive(false);
            mainHub.SetActive(true);

            if (master.BodyElixirs > 0)
            {
                BodyBonus *= ElixirPower;
                UpdateLabels();
                spawner.Spawn(BodyElixirsLabel.transform, $"-1", Color.red);
            }
            int bodyTrained = master.TrainBody(BodyBonus);

            if(bodyTrained > StartBodyBonus) master.BodyElixirs--;

            spawner.Spawn(BodyLabel.transform, $"+{bodyTrained}", Color.green);

            GameCore.Instance.AdvanceTime(1);
        }
    }
}
