using UnityEngine;
using TMPro;

public class StatsBarsManager : MonoBehaviour
{
    private CharacterData character;
    [SerializeField] private TextMeshProUGUI[] labels;
    void Awake()
    {
        var gameCore = FindFirstObjectByType<GameCore>();
        character = gameCore?.Run?.CurrentMasterData;
    }
    void FixedUpdate()
    {
        
    }
}
