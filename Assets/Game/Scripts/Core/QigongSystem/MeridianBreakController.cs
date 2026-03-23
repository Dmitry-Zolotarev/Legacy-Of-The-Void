using UnityEngine;
using TMPro;
public class MeridianBreakController : MonoBehaviour 
{
    [SerializeField] private QiOrbController QiOrb;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI ShootLabel;
    [SerializeField] private TextMeshProUGUI OpenedMeridiansLabel;
    private CharacterData master;

    public static MeridianBreakController Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        master = GameCore.Instance.CurrentMaster;
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        OpenedMeridiansLabel?.SetText($"Открыто меридианов: {master.OpenedMeridians} / {master.MeridianLevels.Count}");
        StartSession();
    }
    public void StartSession()
    {
        QiOrb.StartMoving();
    }
    private void FixedUpdate()
    {
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        OpenedMeridiansLabel?.SetText($"Открыто меридианов: {master.OpenedMeridians} / {master.MeridianLevels.Count}");   

        if (master.Qi > 0) ShootLabel.SetText("Нажмите F для броска");
        else ShootLabel.SetText("Недостаточно ци для броска");
    }
}
