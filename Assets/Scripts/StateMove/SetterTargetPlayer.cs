using UnityEngine;

public class SetterTargetPlayer : SetterTarget
{
    [SerializeField] private TrackingState _tracking;

    protected override bool CanStop()
    {
        return Input.GetMouseButtonUp(0);
    }

    protected override bool CanGoOnGround()
    {
        return Input.GetMouseButton(0) && _positionCheckerPlayer.IsOnGround;
    }

    protected override ITargetable TakeState()
    {
        return _tracking;
    }
}
