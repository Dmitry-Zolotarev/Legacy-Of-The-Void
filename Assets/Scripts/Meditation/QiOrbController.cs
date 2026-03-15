using UnityEngine;

public class QiOrbController : MonoBehaviour
{
    public Transform center;
    public float radius = 2f;
    float angle;

    void Update()
    {
        angle += Time.deltaTime;

        transform.position =
            center.position +
            new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
    }
}