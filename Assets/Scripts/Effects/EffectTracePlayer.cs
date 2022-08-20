using UnityEngine;

public class EffectTracePlayer : EffectTrace
{
    protected override bool PlayEffect(Player player)
    {
        return Input.GetMouseButton(0) && player.IsOnGround;
    }
}
