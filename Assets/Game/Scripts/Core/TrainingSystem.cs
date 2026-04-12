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
    [SerializeField] private float trainingTime = 2f;
    private ParticleSpawner spawner;
    private int BodyBonus = 1;
    private bool IsTraining = false;
    private void Awake()
    {
        spawner = GetComponent<ParticleSpawner>();
        animator.gameObject.SetActive(false);
    }
    private void UpdateLabels()
    {
        BodyElixirsLabel?.SetText(GameCore.Instance.Master.BodyElixirs.ToString());
        QiElixirsLabel?.SetText(GameCore.Instance.Master.QiElixirs.ToString());
    }
    public void TrainBody()
    {
        if(!IsTraining) StartCoroutine(TrainBodyCoroutine());   
    }
    public void OnEnable()
    {
        background.sprite = gymBackground;
        animator.gameObject.SetActive(true);
        UpdateLabels();
    }
    public void OnDisable()
    {
        animator.gameObject.SetActive(false);
        background.sprite = combatBackground;
    }
    private IEnumerator TrainBodyCoroutine()
    {      
        var master = GameCore.Instance.Master;
        BodyBonus = StartBodyBonus;
        if (master.Body < master.MaxBody)
        {
            IsTraining = true;
            animator.SetBool("IsTraining", true);
            yield return new WaitForSeconds(trainingTime / 2);
            
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

            yield return new WaitForSeconds(trainingTime / 2);
            IsTraining = false;
            animator.SetBool("IsTraining", false);
        }
    }
}
