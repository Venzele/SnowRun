using UnityEngine;

public class TwisterSnowballBot : TwisterSnowball
{
    protected override bool PlayEffect(Player player)
    {
        return player.IsRun;
    }
}
