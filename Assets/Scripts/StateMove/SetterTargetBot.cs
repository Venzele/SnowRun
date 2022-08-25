using UnityEngine;

public class SetterTargetBot : SetterTarget
{
    [SerializeField] private WayBotState _wayBot;

    protected override bool CanStop()
    {
        return _positionCheckerPlayer.IsRun == false;
    }

    protected override bool CanGoOnGround()
    {
        return _positionCheckerPlayer.IsOnGround;
    }

    protected override ITargetable TakeState()
    {
        return _wayBot;
    }
}
