
using UnityEngine;

public class InheritanceSystem:MonoBehaviour
{
    public void StartInheritance()
    {
        var run=GameCore.Instance.Run;
        if(run.DiscipleData==null||!run.DiscipleData.DiscipleReadyFlag)return;

        var oldMaster=run.CurrentMasterData;
        run.ArchivedMasters.Add(oldMaster);

        CharacterData newMaster= new CharacterData();
        newMaster.ID=System.Guid.NewGuid().GetHashCode();
        newMaster.Generation=run.GenerationIndex + 1;
        newMaster.Body=oldMaster.Body/2+1;
        newMaster.Qi=oldMaster.Qi/2+1;
        newMaster.Spirit=oldMaster.Spirit/2+1;
        newMaster.MaxQi=oldMaster.MaxQi;
        newMaster.CurrentQi=newMaster.MaxQi;

        run.GenerationIndex++;
        run.CurrentMasterData=newMaster;
        run.DiscipleData=null;
    }
}
