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
        _spawnerSnowball.Spawned += OnStartEffects;
    }

    private void OnDisable()
    {
        _spawnerSnowball.Spawned -= OnStartEffects;
    }

    protected bool PlayEffect(Player player)
    {
        return CanPlayer(player);
    }

    protected virtual IEnumerator GoEffects(SetterSizeSnowball setterSizeSnowball, Player player)
    {
        while (PlayEffect(_player))
            yield return null;
    }

    protected virtual bool CanPlayer(Player player)
    {
        return false;
    }

    private void OnStartEffects()
    {
        StopEffects();

        if (_effects == null)
        {
            _effects = StartCoroutine(GoEffects(_setterSizeSnowball, _player));
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
