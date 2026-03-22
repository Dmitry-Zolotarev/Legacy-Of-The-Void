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
    private bool OnDantian = true;
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

    public void MinusQi(int amount) 
    {
        CarriedQi -= amount;
        if (CarriedQi < 0) CarriedQi = 0;
    }
    public void MoveAlongDantian()
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
        AngleDegrees = (AngleDegrees + CurrentSpeed * Mathf.Deg2Rad) % 360f;
        transform.localPosition = new Vector2(-Mathf.Cos(AngleDegrees * Mathf.Deg2Rad), Mathf.Sin(AngleDegrees * Mathf.Deg2Rad)) * DantianRadius;
    }
    public void MoveAlongMeridian(Vector2 meridian)
    {

    }
    void Update()
    {
        if (OnDantian) MoveAlongDantian();       
    }
}
