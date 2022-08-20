using UnityEngine;

public class SetterTargetPlayer : SetterTarget
{
    [SerializeField] private TrackingState _tracking;

    protected override bool CanStop(Player player)
    {
        return Input.GetMouseButtonUp(0);
    }

    protected override bool CanGoOnGround(Player player)
    {
        return Input.GetMouseButton(0) && player.IsOnGround;
    }

    protected override ITargetable TakeState()
    {
        return _tracking;
    }
}
