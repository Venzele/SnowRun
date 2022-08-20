using UnityEngine;

public class TwisterSnowballPlayer : TwisterSnowball
{
    protected override bool PlayEffect(Player player)
    {
        return Input.GetMouseButton(0) || player.IsOnPlate || player.IsOnStairs;
    }
}
