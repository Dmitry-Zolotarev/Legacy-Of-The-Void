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
public class Enemy
{
    [HideInInspector] public bool IsDead;
    [SerializeField] Sprite ComicSprite;
    
    public void SetComicSprite()
    {
        if(ComicSprite != null) ComicsBook.Instance.Image.sprite = ComicSprite;
    }
}
