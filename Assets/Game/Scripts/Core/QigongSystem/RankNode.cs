using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RankNode : MonoBehaviour
{
    [HideInInspector] public bool isFilled;
    [HideInInspector] public int Qi = 0;
    public int NeedQi = 10;
    [SerializeField] private RectTransform QiOrb;
    private RectTransform rectTransform;
    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        var qiOrb = QiOrb.gameObject.GetComponent<QiOrbController>();

        if (!isFilled && IsOverlap(rectTransform, QiOrb))
        {
            AddQi(qiOrb.CarriedQi);
            qiOrb.OnDantian = true;
            qiOrb.CarriedQi = 0;
        }
    }
    private void AddQi(int amount)
    {
        if (isFilled) return;
        Qi += amount;
        if (Qi >= NeedQi) isFilled = true;
    }
    bool IsOverlap(RectTransform a, RectTransform b)
    {
        Rect rectA = new Rect(a.position, a.rect.size);
        Rect rectB = new Rect(b.position, b.rect.size);
        return rectA.Overlaps(rectB);
    }
}
