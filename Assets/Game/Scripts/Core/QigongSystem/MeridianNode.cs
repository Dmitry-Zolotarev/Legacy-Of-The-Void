using UnityEngine;

public class MeridianNode : MonoBehaviour
{
    public int SealStrength = 10;
    private CharacterData master;
    private RectTransform rect;
    private RectTransform canvasRect;

    private Vector3[] worldCornersA = new Vector3[4];

    private Vector2[] canvasCornersA = new Vector2[4];
    private Vector2[] canvasCornersB = new Vector2[4];

    public void Start()
    {
        master = GameCore.Instance.CurrentMaster;
        rect = GetComponent<RectTransform>();
        canvasRect = GetComponentInParent<Canvas>().transform as RectTransform;
    }

    public void CheckCollision(RectTransform other)
    {
        if (IsOverlappingCanvas(rect, other))
        {
            var qiOrb = other.GetComponent<QiOrbController>();
            if (qiOrb != null)
            {
                SetDamage(qiOrb.CarriedQi);
                qiOrb.CarriedQi = 0;
                qiOrb.OnDantian = true;
            }
        }
    }

    bool IsOverlappingCanvas(RectTransform a, RectTransform b)
    {
        ToCanvasSpace(a, canvasCornersA);
        ToCanvasSpace(b, canvasCornersB);

        float aMinX = canvasCornersA[0].x;
        float aMaxX = canvasCornersA[2].x;
        float aMinY = canvasCornersA[0].y;
        float aMaxY = canvasCornersA[2].y;

        float bMinX = canvasCornersB[0].x;
        float bMaxX = canvasCornersB[2].x;
        float bMinY = canvasCornersB[0].y;
        float bMaxY = canvasCornersB[2].y;

        return aMinX < bMaxX &&
               aMaxX > bMinX &&
               aMinY < bMaxY &&
               aMaxY > bMinY;
    }

    void ToCanvasSpace(RectTransform t, Vector2[] result)
    {
        t.GetWorldCorners(worldCornersA);

        for (int i = 0; i < 4; i++)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                RectTransformUtility.WorldToScreenPoint(null, worldCornersA[i]),
                null,
                out result[i]
            );
        }
    }

    private void SetDamage(int damage)
    {
        SealStrength -= damage;
        if (SealStrength <= 0)
        {
            master.OpenMeridian();
            Destroy(gameObject, 0.1f);
        }
    }
}