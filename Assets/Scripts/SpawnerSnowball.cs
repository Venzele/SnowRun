using UnityEngine;
using UnityEngine.Events;

public class SpawnerSnowball : MonoBehaviour
{
    [SerializeField] private Snowball _template;
    [SerializeField] private SetterTarget _setterTarget;
    [SerializeField] private float _size, _delay;

    private Snowball _newSnowball;
    private bool _isSpawn = false;

    public Snowball NewSnowball => _newSnowball;
    public bool IsSpawn => _isSpawn;
    public float Size => _size;

    public event UnityAction Spawned;

    private void OnEnable()
    {
        _setterTarget.StartedRun += OnSpawn;
    }

    private void OnDisable()
    {
        _setterTarget.StartedRun -= OnSpawn;
    }

    public void ResetSpawn()
    {
        _isSpawn = false;
    }

    private void OnSpawn(PositionCheckerPlayer positionCheckerPlayer)
    {
        if (positionCheckerPlayer.IsOnGround)
        {
            if (_isSpawn == false)
            {
                _newSnowball = Instantiate(_template, positionCheckerPlayer.transform.position, positionCheckerPlayer.transform.rotation, positionCheckerPlayer.transform);
                _newSnowball.transform.localPosition = new Vector3(0, _size / 2, _size / 2);
                _newSnowball.TakeScale(_size);
                _isSpawn = true;
                Spawned?.Invoke();
            }
            else
            {
                Spawned?.Invoke();
            }
        }
    }
}
