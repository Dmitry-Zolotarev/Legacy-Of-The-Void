using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StudentRoomUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NameLabel;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI MeridiansLabel;
    [SerializeField] private GameObject StudyButtons;
    [SerializeField] private MeridianBreakController StudentMeridianBreakthrough;
    
    [SerializeField] private Sprite YoungStudent, AdultStudent;
    [SerializeField] private GameObject StudentGameObject;
    public static StudentRoomUI Instance;
    private Image StudentSprite;
    private Student student;
    void Awake()
    {
        if (Instance == null) Instance = this;
        StudentSprite = StudentGameObject.GetComponent<Image>();
    }
    private void OnEnable() 
    {     
        UpdateLabels();
    }

    private void FixedUpdate() 
    {
        UpdateLabels();
    } 
    private void ToggleElements(bool value)
    {
        StudyButtons.SetActive(value);
        StudentGameObject.SetActive(value);  
        
        if(StudentSprite != null && student != null)
        {
            StudentSprite.sprite = student.Age < 30 ? YoungStudent : AdultStudent;
        }
    }
    private void UpdateLabels()
    {
        student = GameCore.Instance.Master.Student;
        
        if (student != null)
        {
            ToggleElements(true);
            NameLabel?.SetText("Ученик " + student.GetFullName());
            QiLabel?.SetText($"Ци: {student.Qi} / {student.MaxQi}");
            MeridiansLabel?.SetText($"Меридианы: {student.OpenedMeridians} / {GameCore.Instance.Master.OpenedMeridians / 2}");          
        }
        else
        {
            ToggleElements(false);
            NameLabel?.SetText($"Ученик будет назначен после получения ранга «{GameCore.Instance.GetRankForBecomeTeacher()}»");
            QiLabel?.SetText("");
            MeridiansLabel?.SetText("");
        }       
    }
    public void GiveQi() => student.SeedQI(GameCore.Instance.Master);
    
    public void BreakthroughStudentMeridians()
    {
        if (student.OpenedMeridians == GameCore.Instance.Master.OpenedMeridians / 2) return;
        ScreenManager.Instance.OpenMenu((int)Canvases.StudentMeridiansCanvas);
        StudentMeridianBreakthrough.StartSession(student, GameCore.Instance.Master.OpenedMeridians / 2);
    }
}