using System.Collections;
using UnityEngine;

public class TwisterSnowball : MonoBehaviour
{
    [SerializeField] private SpawnerSnowball _spawnerSnowball;
    [SerializeField] private Player _player;
    [SerializeField] private float _speed;

    private Snowball _snowball;
    private Coroutine _rollSnowball;

    private void OnEnable()
    {
        _spawnerSnowball.Spawned += OnRoll;
    }

    private void OnDisable()
    {
        _spawnerSnowball.Spawned -= OnRoll;
    }

    private void OnRoll()
    {
        StopRoll();

        if (_rollSnowball == null)
        {
            _rollSnowball = StartCoroutine(RollSnowball());
        }
    }

    private void StopRoll()
    {
        if (_rollSnowball != null)
        {
            StopCoroutine(_rollSnowball);
            _rollSnowball = null;
        }
    }

    private IEnumerator RollSnowball()
    {
        _snowball = _spawnerSnowball.NewSnowball;

        while (Input.GetMouseButton(0) || _player.IsOnPlate || _player.IsOnStairs || _player.IsBot)
        {
            _snowball.transform.Rotate(Time.deltaTime * _speed, 0, 0);
            yield return null;
        }
    }
}
