
[System.Serializable]
public class InternalDemon
{
    public int Value = 10;
    public int MaxValue = 100;

    public InternalDemonState GetCurrentState()
    {
        var states = GameCore.Instance.InternalDemonStates;
        float i = (float)(Value - 1) / MaxValue * states.Count;
        return states[(int)i];
    }
    public void Change(int n)
    {
        Value += n;
        if (Value > MaxValue) Value = MaxValue;
        if (Value < 1) Value = 1;
    }
}
