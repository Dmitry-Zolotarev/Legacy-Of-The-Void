using UnityEngine;

[System.Serializable]
public class CampaignStage 
{
    public string StageName;
    public string Description;
    public Sprite BackgroundSprite;
    public LootAction LootAction;
    public FightAction FightAction;  
    public RestAction RestAction;
}
