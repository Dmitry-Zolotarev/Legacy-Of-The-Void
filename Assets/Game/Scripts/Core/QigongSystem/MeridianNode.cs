using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MeridianNode : MonoBehaviour
{
    public bool isOpened = false;
    public int MeridianID = 0;
    public int SealStrength = 10;
    public List<Transform> ConnectedMeridians = new List<Transform>();
    private CharacterData master;
    public void Start()
    {
        master = GameCore.Instance.CurrentMaster;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        var qiOrb = collision.gameObject.GetComponent<QiOrbController>();
        if (qiOrb != null) 
        {
            SetDamage(qiOrb.CarriedQi);
            qiOrb.CarriedQi = 0;
        }      
    }
    private void SetDamage(int damage)
    {
        SealStrength -= damage;
        if (SealStrength <= 0 && !isOpened) 
        {
            master.OpenMeridian();
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            isOpened = true;
        }        
    }
}
