using UnityEngine;

public class TwisterSnowballPlayer : TwisterSnowball
{
    protected override bool TakeStatePlayer()
    {
        return Input.GetMouseButton(0) || _positionCheckerPlayer.IsOnPlate || _positionCheckerPlayer.IsOnStairs;
    }
}
