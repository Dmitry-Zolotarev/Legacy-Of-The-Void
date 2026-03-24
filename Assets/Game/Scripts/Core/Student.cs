using UnityEngine;

public class Student : CharacterData
{
    public Student()
    {
        Age = random.Next(12, 17);
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
        Trophies = master.Trophies;
        BodyPills = master.BodyPills;
        SpiritPills = master.SpiritPills;
        QiPills = master.QiPills;
        Ranks = master.Ranks;
        MeridianLevels = master.MeridianLevels;
    }
}
