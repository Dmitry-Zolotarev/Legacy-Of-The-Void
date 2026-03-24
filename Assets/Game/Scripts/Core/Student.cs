using UnityEngine;

public class Student : CharacterData
{
    public Student()
    {
        Age = random.Next(4, 10);
    }
    public void Inherit(CharacterData master)
    {
        Generation = master.Generation + 1;
        Silver = master.Silver;
        Trophies = master.Trophies;
        BodyPills = master.BodyPills;
        SpiritPills = master.SpiritPills;
        QiPills = master.QiPills;
    }
}
