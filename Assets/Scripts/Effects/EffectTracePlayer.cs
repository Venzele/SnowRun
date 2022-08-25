using UnityEngine;

public class EffectTracePlayer : EffectTrace
{
    protected override bool TakeStatePlayer()
    {
        return Input.GetMouseButton(0) && _positionCheckerPlayer.IsOnGround;
    }
}
