using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
}
