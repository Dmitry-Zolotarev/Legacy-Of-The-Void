using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public CharacterData Data => GameManager.Instance.Player;

    public void GainSilver(int amount)
    {
        Data.silver += amount;
    }
}