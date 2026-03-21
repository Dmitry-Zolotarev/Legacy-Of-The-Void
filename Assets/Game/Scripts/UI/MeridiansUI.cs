using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class MeridiansUI : MonoBehaviour
{
    public static MeridiansUI Instance;
    private CharacterData master;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start()
    {
        master = GameCore.Instance.CurrentMaster;
        UpdateUI();
    }
    private void FixedUpdate() => UpdateUI();

    private void UpdateUI()
    {
    }
}