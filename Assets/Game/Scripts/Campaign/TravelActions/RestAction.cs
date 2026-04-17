
[System.Serializable]
public class RestAction : TravelAction
{
    public override void DoAction()
    {       
        base.DoAction();
        GameCore.Instance.Master.RecoverQi();
    }
}
