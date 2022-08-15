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

    protected override bool PlayEffect(Player player)
    {
        return Input.GetMouseButton(0) || player.IsOnPlate || player.IsOnStairs;
    }

    protected override IEnumerator DropSnowflakes(SetterSizeSnowball setterSizeSnowball, Player player)
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
