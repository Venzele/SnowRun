using System.Collections;
using UnityEngine;

public class EffectSnowflakes : SetterEffects
{
    [SerializeField] private ParticleSystem _particle;

    private bool _isPlay;

    private void Start()
    {
        _particle.Stop();
    }

    protected override IEnumerator GoEffects(SetterSizeSnowball setterSizeSnowball, Player player, SpawnerSnowball spawnerSnowball)
    {
        while (PlayEffect(player))
        {
            _particle.transform.localPosition = Vector3.forward * (setterSizeSnowball.NextSize / 2);

            if (setterSizeSnowball.IsSnowball && _isPlay == false)
            {
                _particle.Play();
                _isPlay = true;
            }
            else if (setterSizeSnowball.IsSnowball == false && _isPlay)
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
