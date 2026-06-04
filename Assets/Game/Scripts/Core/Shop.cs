using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(ParticleSpawner))]
[RequireComponent(typeof(PlaySoundsComponent))]
public class Shop : MonoBehaviour
{
    [SerializeField] private int QiElixirPrice = 100;
    [SerializeField] private TextMeshProUGUI SilverAmountLabel;
    [SerializeField] private TextMeshProUGUI QiElixirsLabel;
    [SerializeField] private TextMeshProUGUI QiElixirPriceLabel;
    [SerializeField] private Image Merchant;
    [SerializeField] private Sprite[] MerchantSprites;
    [SerializeField] private List<GameObject> TechniqueLots;
    [SerializeField] private List<TextMeshProUGUI> TechniqueNameLabels;
    [SerializeField] private List<TextMeshProUGUI> TechniquePriceLabels;

    private ParticleSpawner spawner;
    private PlaySoundsComponent audioPlayer;
    private CharacterData master;

    private void Awake()
    {
        spawner = GetComponent<ParticleSpawner>();
        audioPlayer = GetComponent<PlaySoundsComponent>();
    }
    private void OnEnable()
    {
        UpdateUI();
        var merchantGeneration = GameCore.Instance.Year / 30 % MerchantSprites.Length;

        if (Merchant.sprite != MerchantSprites[merchantGeneration])
        {
            Merchant.sprite = MerchantSprites[merchantGeneration];
        }
    }
    
    private void UpdateUI()
    {
        master = GameCore.Instance.Master;

        for (int i = 0; i < TechniqueNameLabels.Count; i++)
        {
            var technique = GameCore.Instance.Techniques[i];
            TechniqueNameLabels[i].SetText(technique.Name);
            TechniquePriceLabels[i].SetText(technique.Price.ToString());
            TechniqueLots[i].gameObject.SetActive(master.CurrentRank >= technique.RequiredRank && !master.KnownTechniques.Contains(technique));       
        }   
        SilverAmountLabel?.SetText(master.Silver.ToString());
        QiElixirsLabel?.SetText(master.QiElixirs.ToString());
        QiElixirPriceLabel?.SetText(QiElixirPrice.ToString());
    }
    public void BuyQiElixir()
    {
        if (master.Silver >= QiElixirPrice)
        {
            master.Silver -= QiElixirPrice;
            master.QiElixirs++;

            spawner.Spawn(SilverAmountLabel.transform, $"-{QiElixirPrice}", Color.red);
            spawner.Spawn(QiElixirsLabel.transform, $"+1", Color.green);
            audioPlayer.Play();
        }
        UpdateUI();
    }
    public void BuyTechnique(int i)
    {
        var technique = GameCore.Instance.Techniques[i];
        if(master.CurrentRank >= technique.RequiredRank && master.Silver >= technique.Price)
        {
            master.Silver -= technique.Price;
            master.KnownTechniques.Add(technique);

            spawner.Spawn(SilverAmountLabel.transform, $"-{technique.Price}", Color.red);
            audioPlayer.Play();
        }
        UpdateUI();
    }
}
