using UnityEngine;

public class EffectSnowflakesPlayer : EffectSnowflakes
{
    protected override bool TakeStatePlayer()
    {
        return Input.GetMouseButton(0) || _positionCheckerPlayer.IsOnPlate || _positionCheckerPlayer.IsOnStairs;
    }
}
