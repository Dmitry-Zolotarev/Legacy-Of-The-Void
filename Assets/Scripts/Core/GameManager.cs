using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CharacterData Player;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Player = new CharacterData();
        Player.stats = new CharacterStats();
        Player.stats.body = 5;
        Player.stats.qi = 5;
        Player.stats.spirit = 5;
        Player.stats.age = 30;
        Player.stats.lifeLimit = 80;
    }
}