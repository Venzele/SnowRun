using System.Collections;
using UnityEngine;

public class SetterSizeSnowball : MonoBehaviour
{
    [SerializeField] private SpawnerSnowball _spawnerSnowball;
    [SerializeField] private SetterTarget _setterTarget;
    [SerializeField] private Builder _builder;
    [SerializeField] private Player _player;
    [SerializeField] private float _stepSize;
    
    private readonly float _maxLevelSnowball = 10;
    private readonly float _cooldownGrowth = 0.9f;
    private readonly float _firstSpeedGrowht = 2;
    private readonly float _secondSpeedGrowth = 0.5f;
    private readonly float _firstSpeedDecrease = 50;
    private readonly float _secondSpeedDecrease = 4;
    private float _levelSnowball, _speedGrowth, _nextSize;
    private Coroutine _giveSizeSnowball;
    private Snowball _snowball;

    public bool IsSnowball { get; private set; }
    public float NextSize => _nextSize;

    private void OnEnable()
    {
        _levelSnowball = 1;
        _nextSize = _spawnerSnowball.Size;
        _spawnerSnowball.Spawned += OnControlSizeSnowball;
        _builder.WentOnBridge += OnControlSizeSnowball;
        _setterTarget.Stoped += OnStopControlSizeSnowball;
    }

    private void OnDisable()
    {
        _spawnerSnowball.Spawned -= OnControlSizeSnowball;
        _builder.WentOnBridge -= OnControlSizeSnowball;
        _setterTarget.Stoped -= OnStopControlSizeSnowball;
    }

    private void OnControlSizeSnowball()
    {
        OnStopControlSizeSnowball();

        if (_giveSizeSnowball == null)
        {
            if (_player.IsOnPlate)
            {
                if (_player.IsOnStairs)
                    _giveSizeSnowball = StartCoroutine(GiveSizeSnowball(-1, 0, _firstSpeedDecrease, _secondSpeedDecrease));
                else if (_player.IsOnBridge)
                    _giveSizeSnowball = StartCoroutine(GiveSizeSnowball(-0.25f, 0, _firstSpeedDecrease, _secondSpeedDecrease));
            }
            else if (_levelSnowball == Mathf.Clamp(_levelSnowball, 1, _maxLevelSnowball) && _player.IsOnGround)
            {
                _giveSizeSnowball = StartCoroutine(GiveSizeSnowball(1, _cooldownGrowth, _firstSpeedGrowht, _secondSpeedGrowth));
            }
        }
    }

    private void OnStopControlSizeSnowball()
    {
        if (_giveSizeSnowball != null)
        {
            StopCoroutine(_giveSizeSnowball);
            _giveSizeSnowball = null;
        }
    }

    private IEnumerator GiveSizeSnowball(float direction, float timeCooldown, float firstSpeed, float secondSpeed)
    {
        _snowball = _spawnerSnowball.NewSnowball;
        float timeRun = 0;
        float currentSize = _nextSize;

        if (_levelSnowball == 1)
            IsSnowball = false;
        else
            IsSnowball = true;

        while (timeCooldown > timeRun)
        {
            timeRun += Time.deltaTime;
            yield return null;
        }

        SelectParamsSnowball(direction, firstSpeed, secondSpeed);
        StartCoroutine(GrowSlow(currentSize));
        _levelSnowball += 1 * direction;
        TryRepeatChangeSize(direction, timeCooldown, firstSpeed, secondSpeed);
    }

    private IEnumerator GrowSlow(float currentSize)
    {
        while (currentSize != _nextSize)
        {
            currentSize = Mathf.MoveTowards(currentSize, _nextSize, _speedGrowth * Time.deltaTime);
            _snowball.TakeScale(currentSize);
            _snowball.transform.localPosition = new Vector3(0, currentSize / 2, currentSize / 2 + _player.LengthHands);
            yield return null;
        }
    }

    private void SelectParamsSnowball(float direction, float firstSpeed, float secondSpeed)
    {
        if ((_levelSnowball == 1 && _player.IsOnGround) || (_levelSnowball <= 2 && _player.IsOnPlate))
        {
            _nextSize += 1 * direction;
            _speedGrowth = firstSpeed;
        }
        else
        {
            _nextSize += _stepSize * direction;
            _speedGrowth = secondSpeed;
        }
    }

    private void TryRepeatChangeSize(float direction, float timeCooldown, float firstSpeed, float secondSpeed)
    {
        if (_player.IsOnPlate)
        {
            if (_levelSnowball <= 1)
            {
                _spawnerSnowball.ResetSpawn();
                _nextSize = 0;
                _levelSnowball = 1;
                IsSnowball = false;
            }

            return;
        }
        else if (_levelSnowball == Mathf.Clamp(_levelSnowball, 1, _maxLevelSnowball) && _player.IsOnGround)
        {
            _giveSizeSnowball = StartCoroutine(GiveSizeSnowball(direction, timeCooldown, firstSpeed, secondSpeed));
        }
    }
}