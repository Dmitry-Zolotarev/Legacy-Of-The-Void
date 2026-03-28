using UnityEngine;

public class FighterCombatStats : MonoBehaviour
{
    [Header("Body")]
    [Min(0)] [SerializeField] protected int bodyLevel = 10;

    [Header("HP Scaling")]
    [Min(0)] [SerializeField] protected int baseHp = 0;
    [Min(0)] [SerializeField] protected int hpPerBodyLevel = 10;

    [Header("Damage Scaling")]
    [Min(0)] [SerializeField] protected int baseDamage = 0;
    [Min(0)] [SerializeField] protected int damagePerBodyLevel = 1;
    [Min(0f)] [SerializeField] protected float partialDamageMultiplier = 0.5f;

    [Header("Qi (set in Inspector)")]
    [Min(0)] [SerializeField] protected int maxQi = 20;
    [Min(0)] protected int currentQi = 0;

    [Header("Unlocked Techniques")]
    [SerializeField] protected bool unlockedDragonFist = true;
    [SerializeField] protected bool unlockedCraneKick = true;
    [SerializeField] protected bool unlockedMoonSlash = true;
    [SerializeField] protected bool unlockedVoidPalm = true;

    public int BodyLevel => bodyLevel;
    public int BaseHp => baseHp;
    public int HpPerBodyLevel => hpPerBodyLevel;
    public int DamagePerBodyLevel => damagePerBodyLevel;
    public float PartialDamageMultiplier => partialDamageMultiplier;

    public int MaxHP => baseHp + bodyLevel * hpPerBodyLevel;
    public int BaseDamage => baseDamage + bodyLevel * damagePerBodyLevel;
    public int PartialDamage => Mathf.RoundToInt(BaseDamage * partialDamageMultiplier);
    public int MaxQi => maxQi;

    public int CurrentHP { get; protected set; }
    public int CurrentQi { get; protected set; }

    public int Rank = 0;

    public virtual void ResetForBattle()
    {
        CurrentHP = MaxHP;
        CurrentQi = MaxQi;
    }

    public void ApplyHp(int value)
    {
        CurrentHP = Mathf.Clamp(value, 0, MaxHP);
    }

    public void ApplyQi(int value)
    {
        CurrentQi = Mathf.Clamp(value, 0, MaxQi);
    }

    public bool HasEnoughQi(int amount)
    {
        return CurrentQi >= amount;
    }

    public virtual bool IsTechniqueUnlocked(TechniqueType techniqueType)
    {
        int i = (int)techniqueType;
        if (GameCore.Instance.Techniques[i].RequiredRank > Rank) return false;

        switch (techniqueType)
        {
            case TechniqueType.DragonFist: return unlockedDragonFist;
            case TechniqueType.CraneKick: return unlockedCraneKick;
            case TechniqueType.MoonSlash: return unlockedMoonSlash;
            case TechniqueType.VoidPalm: return unlockedVoidPalm;
            default: return false;
        }
    }
}
