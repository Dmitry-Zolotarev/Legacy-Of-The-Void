
[System.Serializable]
public class RestAction : TravelAction
{
    public override void DoAction()
    {
        TravelSystem.Instance.UpdateStage();
        GameCore.Instance.Master.RecoverQi();
        
        base.DoAction();
    }
}
