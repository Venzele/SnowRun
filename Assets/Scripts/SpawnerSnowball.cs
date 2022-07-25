using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerSnowball : MonoBehaviour
{
    [SerializeField] private Snowball _template;
    [SerializeField] private SetterTarget _setterTarget;
    [SerializeField] private float _size, _delay;

    private Snowball _newSnowball;
    private bool _isSpawn = false;
    private Coroutine _spawnSnowball;

    public Snowball NewSnowball => _newSnowball;
    public bool IsSpawn => _isSpawn;
    public float Size => _size;

    public event UnityAction Spawned;

    private void OnEnable()
    {
        _setterTarget.StartedRun += OnSpawn;
        _setterTarget.Stoped += OnStopSpawn;
    }

    private void OnDisable()
    {
        _setterTarget.StartedRun -= OnSpawn;
        _setterTarget.Stoped -= OnStopSpawn;
    }

    public void ResetSpawn()
    {
        _newSnowball = null;
        _isSpawn = false;
    }

    private void OnSpawn(Player player)
    {
        if (_spawnSnowball == null)
            _spawnSnowball = StartCoroutine(Spawn(player));
    }

    private void OnStopSpawn()
    {
        if (_spawnSnowball != null)
        {
            StopCoroutine(_spawnSnowball);
            _spawnSnowball = null;
        }
    }

    private IEnumerator Spawn(Player player)
    {
        float timeRun = 0;

        if (_isSpawn == false && _setterTarget.IsGround)
        {
            while (timeRun < _delay)
            {
                timeRun += Time.deltaTime;
                yield return null;
            }

            _newSnowball = Instantiate(_template, player.transform.position, player.transform.rotation, player.transform);
            _newSnowball.transform.localPosition = new Vector3(0, _size / 2, _size / 2 + player.HalfScaleSize);
            _newSnowball.TakeScale(_size);
            _isSpawn = true;
            Spawned?.Invoke();
        }
        else if (_setterTarget.IsGround)
        {
            Spawned?.Invoke();
        }
    }
}
