using UnityEngine;

[RequireComponent(typeof(Transform))]
public class QiOrbController : MonoBehaviour
{
    [SerializeField]private float MinSpeed = 1f;
    [SerializeField]private float MaxSpeed = 2f;
    [SerializeField] private float AccelerationDelta = 1f;
    [SerializeField] private float DantianRadius = 45f;
    
    [HideInInspector] public int CarriedQi = 0;
    [HideInInspector] public float StartSpeed, CurrentSpeed;
    [SerializeField] private bool InBreakthroughMode = false;
    [SerializeField] private int QiAmount = 10;
    public bool OnDantian = true;
    private bool IsMoving = false;
    
    private float AngleDegrees = 0;  
    
    
    void Awake()
    {
        StartSpeed = (MinSpeed + MaxSpeed) / 2f;
        transform.localPosition = new Vector3(0, DantianRadius);
    }
    public float GetSpeedDelta()
    {
        return CurrentSpeed - StartSpeed;
    }
    public void StartMoving()
    {
        IsMoving = true;
        CurrentSpeed = StartSpeed;
    }
    public void StopMoving()
    {
        IsMoving = false;
        CurrentSpeed = 0;
    }
    public void AddQi(int amount) => CarriedQi += amount;

    public void SpendQi(int amount) 
    {
        CarriedQi -= amount;
        if (CarriedQi < 0) CarriedQi = 0;
    }
    public void MoveAlongDantian()
    {
        if (!IsMoving) return;

        AngleDegrees = (AngleDegrees + CurrentSpeed * Mathf.Deg2Rad) % 360f;
        transform.localPosition = new Vector2(-Mathf.Cos(AngleDegrees * Mathf.Deg2Rad), Mathf.Sin(AngleDegrees * Mathf.Deg2Rad)) * DantianRadius;
    }
    public void MoveDirectly()
    {
        if (!IsMoving) 
        {
            OnDantian = true;
            return;
        } 
        transform.localPosition += new Vector3(-Mathf.Cos(AngleDegrees * Mathf.Deg2Rad), Mathf.Sin(AngleDegrees * Mathf.Deg2Rad)) * CurrentSpeed * Mathf.Deg2Rad;
    }
    private void Shoot()
    {
        var master = GameCore.Instance.CurrentMaster;
        if (master == null || master.Qi < 1) return;

        OnDantian = false;     
        CarriedQi = Mathf.Min(master.Qi, QiAmount);
        master.SpendQi(CarriedQi);
    }
    private void Update()
    {
        if (!IsMoving) return;
    
        if (Input.GetMouseButton(0) && CurrentSpeed < MaxSpeed)
        {
            CurrentSpeed += AccelerationDelta * Time.deltaTime;
        }
        if (Input.GetMouseButton(1) && CurrentSpeed > MinSpeed)
        {
            CurrentSpeed -= AccelerationDelta * Time.deltaTime;
        }
        if (InBreakthroughMode && OnDantian && Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }    
        else if(transform.localPosition.magnitude > DantianRadius * 5) OnDantian = true;

        if (OnDantian) MoveAlongDantian();
        else MoveDirectly();
    }
}
