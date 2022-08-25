using System.Collections;
using UnityEngine;

public abstract class SetterEffects : MonoBehaviour
{
    [SerializeField] protected SetterSizeSnowball _setterSizeSnowball;
    [SerializeField] protected SpawnerSnowball _spawnerSnowball;
    [SerializeField] protected PositionCheckerPlayer _positionCheckerPlayer;

    private Coroutine _effects;

    private void OnEnable()
    {
        _spawnerSnowball.Spawned += OnStartEffects;
    }

    private void OnDisable()
    {
        _spawnerSnowball.Spawned -= OnStartEffects;
    }

    protected abstract bool TakeStatePlayer();

    protected abstract IEnumerator GoEffects();

    private void OnStartEffects()
    {
        StopEffects();

        if (_effects == null)
        {
            _effects = StartCoroutine(GoEffects());
        }
    }

    private void StopEffects()
    {
        if (_effects != null)
        {
            StopCoroutine(_effects);
            _effects = null;
        }
    }
}
