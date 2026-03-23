using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class MeridianNode : MonoBehaviour
{
    public int SealStrength = 10;
    private CharacterData master;
    private bool isOpened = false;
    
    [SerializeField] private RectTransform QiOrb;
    private RectTransform rectTransform;

    public void Start()
    {
        master = GameCore.Instance.CurrentMaster;
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        var qiOrb = QiOrb.gameObject.GetComponent<QiOrbController>();

        if (!isOpened && IsOverlap(rectTransform, QiOrb))
        {
            SetDamage(qiOrb.CarriedQi);
            qiOrb.OnDantian = true;
            qiOrb.CarriedQi = 0;
        }
    }
    private void SetDamage(int damage)
    {
        SealStrength -= damage;
        if (!isOpened && SealStrength <= 0)
        {
            isOpened = true;
            master.OpenMeridian();
            Destroy(gameObject, 0.1f);      
        }
    }
    bool IsOverlap(RectTransform a, RectTransform b)
    {
        Rect rectA = new Rect(a.position, a.rect.size);
        Rect rectB = new Rect(b.position, b.rect.size);
        return rectA.Overlaps(rectB);
    }
}