using UnityEngine;

public class QiOrbController
{
    [SerializeField]private float MinSpeed = 1f;
    [SerializeField]private float MaxSpeed = 2f;
    [SerializeField]private float Acceleration = 0.01f;
    private bool IsMoving = false;
    private float CarriedQi = 0f;
    private float Speed;
    
    public QiOrbController()
    {
        Speed = MaxSpeed + MinSpeed / 2f;
    }
    public QiOrbController(int carriedQi)
    {
        Speed = MaxSpeed + MinSpeed / 2f;
        CarriedQi = carriedQi;
    }
    public void MoveAlongPath()
    {
        if (!IsMoving) return;
        if (Input.GetButton("Left")) Speed += Acceleration;
        if (Input.GetButton("Right")) Speed -= Acceleration;
    }
    public void ConsumeAtTarget()
    {

    }
}
