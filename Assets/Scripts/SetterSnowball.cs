using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetterSnowball : MonoBehaviour
{
    [SerializeField] private SpawnerSnowball _spawnerSnowball;
    [SerializeField] private SetterTarget _setterTarget;
    [SerializeField] private Player _player;
    [SerializeField] private float _timeCooldown;
    [SerializeField] private float _stepSize;
    
    private readonly float _maxLevelSnowball = 10;
    private float _levelSnowball, _speedGrowth, _nextSize;
    private Coroutine _giveSizeSnowball;
    private Snowball _snowball;

    private void OnEnable()
    {
        _levelSnowball = 1;
        _spawnerSnowball.Spawned += OnControlSnowball;
        _setterTarget.WentOnBridge += OnControlSnowball;
        _setterTarget.Stoped += OnStopControlSnowball;
    }

    private void OnDisable()
    {
        _spawnerSnowball.Spawned -= OnControlSnowball;
        _setterTarget.WentOnBridge -= OnControlSnowball;
        _setterTarget.Stoped -= OnStopControlSnowball;
    }

    private void Start()
    {
        _nextSize = _spawnerSnowball.Size;
    }

    private void OnControlSnowball()
    {
        OnStopControlSnowball();

        if (_giveSizeSnowball == null)
        {
            if (_setterTarget.IsBridge)
                _giveSizeSnowball = StartCoroutine(GiveSizeSnowball(-1, 0, 5, 2));
            else if (_levelSnowball == Mathf.Clamp(_levelSnowball, 1, _maxLevelSnowball) && _setterTarget.IsGround)
                _giveSizeSnowball = StartCoroutine(GiveSizeSnowball(1, 1.5f, 2, 0.5f));
        }
    }

    private void OnStopControlSnowball()
    {
        if (_giveSizeSnowball != null)
        {
            StopCoroutine(_giveSizeSnowball);
            _giveSizeSnowball = null;
        }
    }

    private IEnumerator GiveSizeSnowball(int direction, float timeCooldown, float firstGrowth, float secondGrowth)
    {
        _snowball = _spawnerSnowball.NewSnowball;
        float timeRun = 0;
        float currentSize = _nextSize;

        while (timeCooldown > timeRun)
        {
            timeRun += Time.deltaTime;
            _snowball.transform.Rotate(Time.deltaTime * 300, 0, 0);
            yield return null;
        }

        SelectParamsSnowball(direction, firstGrowth, secondGrowth);
        StartCoroutine(GrowSlow(currentSize));
        _levelSnowball += 1 * direction;
        TryReplayGrowth(direction, timeCooldown, firstGrowth, secondGrowth);
    }

    private IEnumerator GrowSlow(float currentSize)
    {
        while (currentSize != _nextSize)
        {
            currentSize = Mathf.MoveTowards(currentSize, _nextSize, _speedGrowth * Time.deltaTime);
            _snowball.TakeScale(currentSize);
            _snowball.transform.localPosition = new Vector3(0, currentSize / 2, currentSize / 2 + _player.HalfScaleSize);
            yield return null;
        }
    }

    private void SelectParamsSnowball(int direction, float firstGrowth, float secondGrowth)
    {
        if ((_levelSnowball == 1 && _setterTarget.IsGround) || _levelSnowball == 2 && _setterTarget.IsBridge)
        {
            _nextSize += 1 * direction;
            _speedGrowth = firstGrowth;
        }
        else
        {
            _nextSize += _stepSize * direction;
            _speedGrowth = secondGrowth;
        }
    }

    private void TryReplayGrowth(int direction, float timeCooldown, float firstGrowth, float secondGrowth)
    {
        if (_setterTarget.IsBridge)
        {
            if (_levelSnowball < 1)
            {
                _snowball = null;
                _spawnerSnowball.ResetSpawn();
                _nextSize = 0.1f;
                _levelSnowball = 1;
            }

            return;
        }
        else if (_levelSnowball == Mathf.Clamp(_levelSnowball, 1, _maxLevelSnowball) && _setterTarget.IsGround)
        {
            _giveSizeSnowball = StartCoroutine(GiveSizeSnowball(direction, timeCooldown, firstGrowth, secondGrowth));
        }
    }
}
