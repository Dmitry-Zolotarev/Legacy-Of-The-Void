using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ParticleSpawner))]
public class TrainingSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI QiElixirsLabel;
    [SerializeField] private TextMeshProUGUI BodyLabel;  
    [SerializeField] private Animator animator;
    [SerializeField] private Image background;
    [SerializeField] private Sprite gymBackground;
    [SerializeField] private Sprite combatBackground;
    [SerializeField] private GameObject AgePanel;
    [SerializeField] private GameObject UI;


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
            animator?.SetBool("IsTraining", true);
            AgePanel?.SetActive(false);
            UI?.SetActive(false);

            yield return new WaitForSeconds(trainingTime);

            IsTraining = false;
            animator?.SetBool("IsTraining", false);
            AgePanel?.SetActive(true);
            UI?.SetActive(true);

            int bodyTrained = master.TrainBody(BodyBonus);       
            spawner.Spawn(BodyLabel.transform, $"+{bodyTrained}", Color.green);

            master.InternalDemon.Change(InternalDemonIncrease);

            GameCore.Instance.AdvanceTime(SpendMonths);

            
            
        }
    }
}
