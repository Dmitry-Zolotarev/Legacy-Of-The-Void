using UnityEngine;

public class Student : CharacterData
{
    public Student()
    {
        Age = Random.Range(10, 13);
    }
    public Student(SaveData data)
    {
        Age = data.StudentAge;
        LifeLimit = data.StudentLifeLimit;
        Name = data.StudentName;
        Qi = data.StudentQi;
        MaxQi = data.StudentMaxQi;
        OpenedMeridians = data.StudentOpenedMeridians;       
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
