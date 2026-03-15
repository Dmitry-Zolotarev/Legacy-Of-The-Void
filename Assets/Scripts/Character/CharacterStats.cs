[System.Serializable]
public class CharacterStats
{
    public int body;
    public int qi;
    public int spirit;

    public int age;
    public int lifeLimit;

    public bool isAlive => age < lifeLimit;
}