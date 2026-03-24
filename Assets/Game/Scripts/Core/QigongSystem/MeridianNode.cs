using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class MeridianNode : MonoBehaviour
{
    public int StartSealStrength = 20;
    private int SealStrength;
    public CharacterData master;
    private bool IsOpened = false;
    
    [SerializeField] private RectTransform QiOrb;
    private RectTransform rectTransform;

    public void Start()
    {
        SealStrength = StartSealStrength;
        master = GameCore.Instance.CurrentMaster;
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        var qiOrb = QiOrb.gameObject.GetComponent<QiOrbController>();

        if (!IsOpened && IsOverlap(rectTransform, QiOrb))
        {           
            qiOrb.OnDantian = true;
            int damage = qiOrb.CarriedQi;
            qiOrb.CarriedQi = 0;
            SetDamage(damage);
        }
    }
    private void SetDamage(int damage)
    {
        SealStrength -= damage;
        if (!IsOpened && SealStrength <= 0)
        {
            IsOpened = true;
            master.OpenMeridian();
            gameObject.SetActive(false);    
        }
    }
    bool IsOverlap(RectTransform a, RectTransform b)
    {
        Rect rectA = new Rect(a.position, a.rect.size);
        Rect rectB = new Rect(b.position, b.rect.size);
        return rectA.Overlaps(rectB);
    }
    public void UpdateNode()
    {
        SealStrength = StartSealStrength;
        IsOpened = false;
    }
}