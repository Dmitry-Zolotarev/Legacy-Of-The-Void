using UnityEngine;

public class Student : CharacterData
{
    public Student()
    {
        Age = Random.Range(10, 15);
    }
    public void SeedQI(CharacterData master)
    {
        var amount = Mathf.Min(MaxQi - Qi, master.Qi);
        master.SpendQi(amount);
        AddQi(amount);
    }
    public void Inherit(CharacterData master)
    {
        Generation = master.Generation + 1;
        Silver = master.Silver;
        BodyElixirs = master.BodyElixirs;
        QiElixirs = master.QiElixirs;
    }
}
