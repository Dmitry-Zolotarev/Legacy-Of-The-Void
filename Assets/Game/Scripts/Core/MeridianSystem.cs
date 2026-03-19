
using System.Collections.Generic;
using UnityEngine;

public class MeridianSystem : MonoBehaviour
{
    public List<Meridian> meridians;
    public Dictionary<int, MeridianState> states = new Dictionary<int, MeridianState>();

    public int selectedMeridianId = -1;

    public void Initialize()
    {
        foreach (var m in meridians) states[m.id] = MeridianState.Hidden;
    }

    public List<int> GetVisibleMeridians()
    {
        List<int> result = new List<int>();

        foreach (var m in meridians)
        {
            if (states[m.id] == MeridianState.VisibleLocked)
                result.Add(m.id);
        }

        return result;
    }

    public void OpenMeridian(int id)
    {
        states[id] = MeridianState.Opened;
    }
}
