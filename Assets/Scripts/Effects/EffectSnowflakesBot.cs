using UnityEngine;

public class EffectSnowflakesBot : EffectSnowflakes
{
    protected override bool PlayEffect(Player player)
    {
        return player.IsRun;
    }
}
