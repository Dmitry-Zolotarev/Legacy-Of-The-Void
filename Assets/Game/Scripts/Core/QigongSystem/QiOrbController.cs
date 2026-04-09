using UnityEngine;

[RequireComponent(typeof(Transform))]
public class QiOrbController : MonoBehaviour
{
    public float MinSpeed = 25f;
    public float MaxSpeed = 25f;
    [SerializeField] private float DantianRadius = 70f;
    
    [HideInInspector] public float StartSpeed, CurrentSpeed;
    [HideInInspector] public bool OnDantian = true;
    [SerializeField] private bool InBreakthroughMode = false;   
    public float AngleDegrees = 0;
    public int QiAmount = 5;

    private void OnEnable()
    {
        StartSpeed = (MinSpeed + MaxSpeed) / 2f;
        transform.localPosition = new Vector3(0, DantianRadius);
        CurrentSpeed = StartSpeed;
    }
    private void OnDisable()
    {
        OnDantian = true;
    }
    public float GetSpeedDelta()
    {
        return CurrentSpeed - StartSpeed;
    }
    public void MoveAlongDantian()
    {
        float deltaSpeed = CurrentSpeed;

        if (!InBreakthroughMode) deltaSpeed *= deltaSpeed;//„тобы ускорение и замедление шара были заметнее

        AngleDegrees = (AngleDegrees + deltaSpeed * Time.deltaTime * 5f) % 360f;
        transform.localPosition = new Vector2(-Mathf.Cos(AngleDegrees * Mathf.Deg2Rad), Mathf.Sin(AngleDegrees * Mathf.Deg2Rad)) * DantianRadius;
    }
    public void MoveDirectly()
    {
        transform.localPosition += new Vector3(-Mathf.Cos(AngleDegrees * Mathf.Deg2Rad), Mathf.Sin(AngleDegrees * Mathf.Deg2Rad)) * CurrentSpeed * Time.deltaTime * 10f;
    }
    private void Shoot()
    {
        if (GameCore.Instance.Master.Qi >= QiAmount)
        {
            OnDantian = false;
            GameCore.Instance.Master.SpendQi(QiAmount);
        }       
    }
    private void Update()
    {   
        if (InBreakthroughMode)
        {
            if(Input.GetMouseButtonDown(1) && OnDantian) Shoot();

            if (transform.localPosition.magnitude > DantianRadius * 7f) OnDantian = true;
        }    
        else
        {
            if (Input.GetMouseButton(0) && CurrentSpeed < MaxSpeed) CurrentSpeed += Time.deltaTime;

            if (Input.GetMouseButton(1) && CurrentSpeed > MinSpeed) CurrentSpeed -= Time.deltaTime;
        }
        if (OnDantian) MoveAlongDantian();
        else MoveDirectly();  
    }
}
