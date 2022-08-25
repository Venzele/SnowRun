using UnityEngine;

public class EffectTraceBot : EffectTrace
{
    protected override bool TakeStatePlayer()
    {
        return _positionCheckerPlayer.IsRun && _positionCheckerPlayer.IsOnGround;
    }
}
