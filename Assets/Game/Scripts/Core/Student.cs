using UnityEngine;

[System.Serializable]
public class Student : CharacterData
{
    public Student()
    {
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
        Silver = master.Silver;
        QiElixirs = master.QiElixirs;
    }
}
