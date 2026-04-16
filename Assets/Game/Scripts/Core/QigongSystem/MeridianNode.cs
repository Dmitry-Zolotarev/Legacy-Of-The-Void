using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class MeridianNode : MonoBehaviour
{
    public int StartSealStrength = 15;
    private int SealStrength;
    public bool IsOpened = false;     
    private RectTransform rectTransform;

    [SerializeField] private RectTransform QiOrb;
    [SerializeField] private Sprite NodeBreakStrong;
    [SerializeField] private Sprite NodeBreakWeak;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Image nodeImage;
    [SerializeField] private int InternalDemonIncrease = 2;
    public void Start()
    {
        SealStrength = StartSealStrength;
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        var qiOrb = QiOrb.gameObject.GetComponent<QiOrbController>();
        if (!IsOpened && IsOverlap(rectTransform, QiOrb))
        {           
            qiOrb.OnDantian = true;
            SetDamage(qiOrb.QiAmount);
        }
        if (SealStrength <= StartSealStrength * 2f / 3f) nodeImage.sprite = NodeBreakStrong;
        if (SealStrength <= StartSealStrength / 3f) nodeImage.sprite = NodeBreakWeak;
    }
    private void SetDamage(int damage)
    {
        SealStrength -= damage;       
        if (!IsOpened && SealStrength <= 0)
        {
            IsOpened = true;
            gameObject.SetActive(false);
            GameCore.Instance.Master.InternalDemon.Increase(2);
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
        nodeImage.sprite = defaultSprite;
        SealStrength = StartSealStrength;
        IsOpened = false;
    }
}