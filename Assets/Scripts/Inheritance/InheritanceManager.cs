using UnityEngine;

public class InheritanceManager : MonoBehaviour
{
    public static InheritanceManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void TriggerInheritance()
    {
        var disciple = DiscipleManager.Instance.disciple;

        if (disciple == null)
        {
            Debug.Log("Lineage ended.");
            return;
        }

        CreateNextCharacter(disciple);
    }

    void CreateNextCharacter(DiscipleData data)
    {
        CharacterData newChar = new CharacterData();
        newChar.stats = new CharacterStats();

        newChar.stats.body = 5;
        newChar.stats.qi = data.storedQi;
        newChar.stats.spirit = 5;

        GameManager.Instance.Player = newChar;

        Debug.Log("New generation started");
    }
}