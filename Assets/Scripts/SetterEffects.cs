using System.Collections;
using UnityEngine;

public class SetterEffects : MonoBehaviour
{
    [SerializeField] private SetterSizeSnowball _setterSizeSnowball;
    [SerializeField] private SpawnerSnowball _spawnerSnowball;
    [SerializeField] private Player _player;

    private Coroutine _effects;

    private void OnEnable()
    {
        _spawnerSnowball.Spawned += OnStartSnowflakes;
    }

    private void OnDisable()
    {
        _spawnerSnowball.Spawned -= OnStartSnowflakes;
    }

    protected virtual bool PlayEffect(Player player)
    {
        return false;
    }

    protected virtual IEnumerator DropSnowflakes(SetterSizeSnowball setterSizeSnowball, Player player)
    {
        while (PlayEffect(_player))
            yield return null;
    }

    private void OnStartSnowflakes()
    {
        StopSnowflakes();

        if (_effects == null)
        {
            _effects = StartCoroutine(DropSnowflakes(_setterSizeSnowball, _player));
        }
    }

    private void StopSnowflakes()
    {
        if (_effects != null)
        {
            StopCoroutine(_effects);
            _effects = null;
        }
    }
}
