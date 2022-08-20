using System.Collections;
using UnityEngine;

public class TwisterSnowball : SetterEffects
{
    [SerializeField] private float _speed;

    private Snowball _snowball;

    protected override IEnumerator GoEffects(SetterSizeSnowball setterSizeSnowball, Player player, SpawnerSnowball spawnerSnowball)
    {
        _snowball = spawnerSnowball.NewSnowball;

        while (PlayEffect(player))
        {
            _snowball.transform.Rotate(Time.deltaTime * _speed, 0, 0);
            yield return null;
        }
    }
}
