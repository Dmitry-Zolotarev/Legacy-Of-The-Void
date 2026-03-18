
using UnityEngine;

public class MeditationInputHandler : MonoBehaviour
{
    public float acceleration = 5f;
    public float deceleration = 5f;

    public float GetSpeedDelta()
    {
        float delta = 0f;

        if (Input.GetMouseButton(1))
            delta += acceleration * Time.deltaTime;

        if (Input.GetMouseButton(0))
            delta -= deceleration * Time.deltaTime;

        return delta;
    }
}
