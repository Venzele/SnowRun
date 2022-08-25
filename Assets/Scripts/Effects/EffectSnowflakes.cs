using System.Collections;
using UnityEngine;

public abstract class EffectSnowflakes : SetterEffects
{
    [SerializeField] private ParticleSystem _particle;

    private bool _isPlay;

    private void Start()
    {
        _particle.Stop();
    }

    protected override IEnumerator GoEffects()
    {
        while (TakeStatePlayer())
        {
            _particle.transform.localPosition = Vector3.forward * (_setterSizeSnowball.NextSize / 2);

            if (_setterSizeSnowball.IsSnowball && _isPlay == false)
            {
                _particle.Play();
                _isPlay = true;
            }
            else if (_setterSizeSnowball.IsSnowball == false && _isPlay)
            {
                _particle.Stop();
                _isPlay = false;
            }

            yield return null;
        }

        _particle.Stop();
        _isPlay = false;
    }
}
