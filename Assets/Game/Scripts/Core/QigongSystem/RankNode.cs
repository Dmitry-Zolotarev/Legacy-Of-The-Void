using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class RankNode : MonoBehaviour
{
    [HideInInspector] public bool IsFilled = false;
    [HideInInspector] public int Qi = 0;
    public int NeedQi = 10;
    [SerializeField] private RectTransform QiOrb;
    private RectTransform rectTransform;
    private Image image;
    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }
    private void Update()
    {
        var qiOrb = QiOrb.gameObject.GetComponent<QiOrbController>();

        image.color = IsFilled ? Color.aquamarine : Color.white;
        
        if (!IsFilled && IsOverlap(rectTransform, QiOrb))
        {
            AddQi(qiOrb.CarriedQi);
            qiOrb.OnDantian = true;
            qiOrb.CarriedQi = 0;
        }
    }
    private void AddQi(int amount)
    {
        if (IsFilled) return;
        Qi += amount;
        if (Qi >= NeedQi) IsFilled = true;
    }
    bool IsOverlap(RectTransform a, RectTransform b)
    {
        Rect rectA = new Rect(a.position, a.rect.size);
        Rect rectB = new Rect(b.position, b.rect.size);
        return rectA.Overlaps(rectB);
    }
    public void ClearNode()
    {
        Qi = 0;
        IsFilled = false;
    }
}
