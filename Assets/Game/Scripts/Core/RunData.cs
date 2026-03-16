
using System;
using System.Collections.Generic;

[Serializable]
public class RunData
{
    public string RunId;
    public int GenerationIndex;
    public string CurrentMasterId;
    public string RunState;
    public bool VictoryFlag;
    public bool DefeatFlag;
    public CharacterData CurrentMasterData;
    public DiscipleData DiscipleData;
    public List<CharacterData> ArchivedMasters=new List<CharacterData>();
}
