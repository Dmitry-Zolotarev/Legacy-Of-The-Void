using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ParticleSpawner))]
[RequireComponent(typeof(PlaySoundsComponent))]
public class TrainingSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI QiElixirsLabel;
    [SerializeField] private TextMeshProUGUI BodyLabel;  
    [SerializeField] private Animator animator;
    [SerializeField] private Image background;
    [SerializeField] private Sprite gymBackground;
    [SerializeField] private Sprite combatBackground;
    [SerializeField] private float trainingTime = 3f;
    [SerializeField] private int SpendMonths = 4;
    [SerializeField] private int InternalDemonIncrease = 4;

    private ParticleSpawner spawner;
    private PlaySoundsComponent audioPlayer;
    public bool IsTraining = false;

    private void Awake()
    {
        spawner = GetComponent<ParticleSpawner>();
        audioPlayer = GetComponent<PlaySoundsComponent>();
        animator.gameObject.SetActive(false);
    }
    private void UpdateLabels()
    {
        QiElixirsLabel?.SetText(GameCore.Instance.Master.QiElixirs.ToString());
    }
    public void TrainBody()
    {
        if (!IsTraining) StartCoroutine(TrainBodyCoroutine());
        else IsTraining = false;
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

        if(master.Body < master.MaxBody)
        {
            IsTraining = true;
            animator?.SetBool("IsTraining", true);
        }        
        while (master.Body < master.MaxBody && IsTraining)
        {
            IsTraining = true;           
            yield return new WaitForSeconds(trainingTime);
            int bodyTrained = master.TrainBody();    
            spawner.Spawn(BodyLabel.transform, $"+{bodyTrained}", Color.green);
            master.InternalDemon.Change(InternalDemonIncrease);
            GameCore.Instance.AdvanceTime(SpendMonths);
            
        }   
        if(IsTraining) audioPlayer.Play();
        animator?.SetBool("IsTraining", false);
        IsTraining = false;
    }
}
