using UnityEngine;

public class TwisterSnowballBot : TwisterSnowball
{
    protected override bool TakeStatePlayer()
    {
        return _positionCheckerPlayer.IsRun;
    }
}
