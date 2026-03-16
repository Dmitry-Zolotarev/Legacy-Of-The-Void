
using System;
using System.Collections.Generic;

[Serializable]
public class DiscipleData
{
    public string DiscipleId;
    public bool DiscipleUnlockedFlag;
    public int DiscipleQiReserve;
    public bool DiscipleReadyFlag;
    public List<string> DiscipleLearnedTechniques=new List<string>();
    public List<int> DisciplePreparedMeridians=new List<int>();
}
