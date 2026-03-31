using UnityEngine;


public enum Demons
{
    GunHaRen,
    MokJinHo,
    SaYunVol,
    ChonMa,
    NoDemon
}
[System.Serializable]
public class Demon
{
    [HideInInspector] public bool IsDead;
    public Sprite Sprite;
    [SerializeField] Sprite ComicSprite;
    
    public void SetComicSprite()
    {
        if(ComicSprite != null) ComicsBook.Instance.Image.sprite = ComicSprite;
    }
}
