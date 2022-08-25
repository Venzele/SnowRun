using System.Collections;
using UnityEngine;

public abstract class TwisterSnowball : SetterEffects
{
    [SerializeField] private float _speed;

    private Snowball _snowball;

    protected override IEnumerator GoEffects()
    {
        _snowball = _spawnerSnowball.NewSnowball;

        while (TakeStatePlayer())
        {
            _snowball.transform.Rotate(Time.deltaTime * _speed, 0, 0);
            yield return null;
        }
    }
}
