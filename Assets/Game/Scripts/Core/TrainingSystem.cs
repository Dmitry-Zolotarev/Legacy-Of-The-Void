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
    [SerializeField] private Animator animator;
    [SerializeField] private Image background;
    [SerializeField] private Sprite gymBackground;
    [SerializeField] private Sprite combatBackground;

    [SerializeField] private float trainingTime = 4f;
    [SerializeField] private int ElixirPower = 2;
    [SerializeField] private int SpendMonths = 6;
    [SerializeField] private int InternalDemonIncrease = 4;

    private ParticleSpawner spawner;
    private int BodyBonus = 1;
    public bool IsTraining = false;
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
        IsTraining = false;
        animator.SetBool("IsTraining", false);
        background.sprite = gymBackground;
        animator.gameObject.SetActive(true);
        UpdateLabels();
    }
    private void FixedUpdate()
    {
        UpdateLabels();
    }
    private void OnDisable()
    {
        animator.gameObject.SetActive(false);
        background.sprite = combatBackground;
    }
    private IEnumerator TrainBodyCoroutine()
    {      
        var master = GameCore.Instance.Master;
        BodyBonus = 1;
        if (master.Body < master.MaxBody)
        {
            IsTraining = true;
            animator.SetBool("IsTraining", true);
            yield return new WaitForSeconds(trainingTime * 0.5f);
            
            if (master.BodyElixirs > 0)
            {
                BodyBonus *= ElixirPower;
                master.BodyElixirs--;
                spawner.Spawn(BodyElixirsLabel.transform, $"-1", Color.red);
            }
            int bodyTrained = master.TrainBody(BodyBonus);       
            spawner.Spawn(BodyLabel.transform, $"+{bodyTrained}", Color.green);

            master.InternalDemon.Increase(InternalDemonIncrease);

            GameCore.Instance.AdvanceTime(SpendMonths);

            yield return new WaitForSeconds(trainingTime * 0.5f);
            IsTraining = false;
            animator.SetBool("IsTraining", false);
            
        }
    }
}
