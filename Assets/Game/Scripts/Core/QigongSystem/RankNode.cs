using System.Collections.Generic;
using UnityEngine;

public class RankNode : MonoBehaviour
{
    [HideInInspector] public bool isFilled;
    [HideInInspector] public int Qi = 0;
    public int NeedQi = 10;
    public List<Transform> ConnectedMeridians = new List<Transform>();
    public void OnCollisionEnter2D(Collision2D collision)
    {
        var qiOrb = collision.gameObject.GetComponent<QiOrbController>();
        if (qiOrb != null)
        {
            AddQi(qiOrb.CarriedQi);
            qiOrb.CarriedQi = 0;
        }
    }
    private void AddQi(int amount)
    {
        if (isFilled) return;
        Qi += amount;
        if (Qi >= NeedQi) isFilled = true;
    }
}
