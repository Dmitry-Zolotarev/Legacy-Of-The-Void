using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class CombatBackground : MonoBehaviour
{
    [HideInInspector] public Image backroundImage;
    public static CombatBackground Instance;
    void Awake()
    {
        backroundImage = GetComponent<Image>();
        if (Instance == null) Instance = this;
    }
}
