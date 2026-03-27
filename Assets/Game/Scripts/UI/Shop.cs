using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    [SerializeField] private int BodyElixirPrice = 50;
    [SerializeField] private int QiElixirPrice = 100;
    [SerializeField] private TextMeshProUGUI SilverAmountLabel;
    [SerializeField] private TextMeshProUGUI BodyElixirsLabel;
    [SerializeField] private TextMeshProUGUI QiElixirsLabel;
    [SerializeField] private TextMeshProUGUI BodyElixirPriceLabel;
    [SerializeField] private TextMeshProUGUI QiElixirPriceLabel;
    [SerializeField] private Image Merchant;
    [SerializeField] private Sprite[] MerchantSprites;
    [SerializeField] private List<GameObject> TechniqueLots;
    [SerializeField] private List<TextMeshProUGUI> TechniqueNameLabels;
    [SerializeField] private List<TextMeshProUGUI> TechniquePriceLabels;
    private CharacterData master;

    private void OnEnable()
    {
        
        UpdateUI();
    }
    private void Update()
    { 
        var merchantGeneration = GameCore.Instance.Year / 30 % MerchantSprites.Length;
        Debug.Log(merchantGeneration);
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
            var technique = GameCore.Instance.Techniques[i + 1];
            TechniqueNameLabels[i].SetText(technique.Name);
            TechniquePriceLabels[i].SetText(technique.Price.ToString());
            TechniqueLots[i].gameObject.SetActive(master.CurrentRank >= technique.RequiredRank && !master.KnownTechniques.Contains(technique));       
        }   
        SilverAmountLabel?.SetText(master.Silver.ToString());
        BodyElixirsLabel?.SetText(master.BodyElixirs.ToString());
        QiElixirsLabel?.SetText(master.QiElixirs.ToString());
        BodyElixirPriceLabel?.SetText(BodyElixirPrice.ToString());
        QiElixirPriceLabel?.SetText(QiElixirPrice.ToString());
    }
    public void BuyQiElixir()
    {
        if(master.Silver >= QiElixirPrice)
        {
            master.Silver -= QiElixirPrice;
            master.QiElixirs++;
        }
        UpdateUI();
    }
    public void BuyBodyElixir()
    {
        if (master.Silver >= BodyElixirPrice)
        {
            master.Silver -= BodyElixirPrice;
            master.BodyElixirs++;
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
        }
        UpdateUI();
    }
}
