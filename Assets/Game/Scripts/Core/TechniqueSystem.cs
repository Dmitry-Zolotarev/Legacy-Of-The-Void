
using UnityEngine;

public class TechniqueSystem:MonoBehaviour
{
    public void Learn(string id)
    {
        var m=GameCore.Instance.Master;
        if(!m.KnownTechniques.Contains(id)) m.KnownTechniques.Add(id);

    }

    public void Equip(string id)
    {
        var m=GameCore.Instance.Master;
        if(m.KnownTechniques.Contains(id)&&!m.EquippedTechniques.Contains(id)&&m.EquippedTechniques.Count<2) m.EquippedTechniques.Add(id);
    }
}
