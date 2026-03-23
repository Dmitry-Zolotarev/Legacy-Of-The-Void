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
    public float AngleDegrees = 0;
    private bool IsMoving = false;
    private bool InBottom = false;
    private bool PassedBottom = false;


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

        AngleDegrees = (AngleDegrees + CurrentSpeed * Time.deltaTime * 10f) % 360f;
        transform.localPosition = new Vector2(-Mathf.Cos(AngleDegrees * Mathf.Deg2Rad), Mathf.Sin(AngleDegrees * Mathf.Deg2Rad)) * DantianRadius;
    }
    public void MoveDirectly()
    {
        if (!IsMoving) 
        {
            OnDantian = true;
            return;
        } 
        transform.localPosition += new Vector3(-Mathf.Cos(AngleDegrees * Mathf.Deg2Rad), Mathf.Sin(AngleDegrees * Mathf.Deg2Rad)) * CurrentSpeed * Time.deltaTime * 10f;
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
    
        if (InBreakthroughMode)
        {
            if(OnDantian && Input.GetKeyDown(KeyCode.F)) Shoot();
            else if (transform.localPosition.magnitude > DantianRadius * 6f) OnDantian = true;
        }    
        else
        {
            if (Input.GetMouseButton(0) && CurrentSpeed < MaxSpeed)
            {
                CurrentSpeed += AccelerationDelta * Time.deltaTime;
            }
            if (Input.GetMouseButton(1) && CurrentSpeed > MinSpeed)
            {
                CurrentSpeed -= AccelerationDelta * Time.deltaTime;
            }
        }
        if (OnDantian) MoveAlongDantian();
        else MoveDirectly();

        if (AngleDegrees > 269f) InBottom = true;
        if (AngleDegrees > 271f)
        {
            InBottom = false;
            PassedBottom = false;
        }        
    }
    public bool PassBottom()
    {
        if (!InBottom || PassedBottom) return false;
        PassedBottom = true;
        return true;
    }
}
