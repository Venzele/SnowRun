using UnityEngine;

public class SetterTargetBot : SetterTarget
{
    [SerializeField] private WayBotState _wayBot;

    protected override bool CanStop(Player player)
    {
        return player.IsRun == false;
    }

    protected override bool CanGoOnGround(Player player)
    {
        return player.IsOnGround;
    }

    protected override ITargetable TakeState()
    {
        return _wayBot;
    }
}
