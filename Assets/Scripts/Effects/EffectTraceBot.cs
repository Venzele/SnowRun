using UnityEngine;

public class EffectTraceBot : EffectTrace
{
    protected override bool PlayEffect(Player player)
    {
        return player.IsRun && player.IsOnGround;
    }
}
