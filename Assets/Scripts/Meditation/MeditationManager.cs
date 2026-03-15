using UnityEngine;

public class MeditationManager : MonoBehaviour
{
    public static MeditationManager Instance;

    float timer;
    public float sessionDuration = 10f;

    float rhythmScore;
    bool active;

    void Awake()
    {
        Instance = this;
    }

    public void StartMeditation()
    {
        timer = sessionDuration;
        rhythmScore = 0;
        active = true;
    }

    void Update()
    {
        if (!active) return;

        timer -= Time.deltaTime;

        if (Input.GetMouseButton(1))
            rhythmScore += Time.deltaTime;

        if (Input.GetMouseButton(0))
            rhythmScore -= Time.deltaTime;

        if (timer <= 0)
            FinishMeditation();
    }

    void FinishMeditation()
    {
        active = false;

        int qi = Mathf.RoundToInt(rhythmScore * 10);

        GameManager.Instance.Player.AddQi(qi);

        MeridianSystem.Instance.CheckBreakthrough(rhythmScore);
    }
}