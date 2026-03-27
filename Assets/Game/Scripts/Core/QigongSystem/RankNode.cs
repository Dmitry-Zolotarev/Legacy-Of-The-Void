using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class RankNode : MonoBehaviour
{
    [HideInInspector] public bool IsFilled = false;
    [HideInInspector] public int Qi = 0;
    public int NeedQi = 15;
    [SerializeField] private RectTransform QiOrb;
    private RectTransform rectTransform;
    [SerializeField] private Image FillImage;
    [SerializeField] private Color QiColor = Color.aquamarine;
    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        FillImage.color = QiColor;
    }
    private void OnEnable()
    {
        FillImage.gameObject.SetActive(true);
        FillImage.fillAmount = (float)Qi / NeedQi;
    }
    private void OnDisable()
    {
        FillImage.gameObject.SetActive(false);
    }
    private void Update()
    {
        var qiOrb = QiOrb.gameObject.GetComponent<QiOrbController>();

        FillImage.fillAmount = (float)Qi / NeedQi;
        
        if (!IsFilled && IsOverlap(rectTransform, QiOrb))
        {
            AddQi(qiOrb.QiAmount);
            qiOrb.OnDantian = true;
        }
    }
    private void AddQi(int amount)
    {
        if (IsFilled) return;
        Qi += amount;
        if (Qi >= NeedQi)
        {
            Qi = NeedQi;
            IsFilled = true;
        }
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
