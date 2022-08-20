using UnityEngine;

public class EffectSnowflakesPlayer : EffectSnowflakes
{
    protected override bool PlayEffect(Player player)
    {
        return Input.GetMouseButton(0) || player.IsOnPlate || player.IsOnStairs;
    }
}
