using UnityEngine;

public class EffectSnowflakesBot : EffectSnowflakes
{
    protected override bool TakeStatePlayer()
    {
        return _positionCheckerPlayer.IsRun;
    }
}
