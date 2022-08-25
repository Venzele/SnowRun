using SplineMesh;
using System.Collections;
using UnityEngine;

public abstract class EffectTrace : SetterEffects
{
    [SerializeField] private WayTrace _wayTrace;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _directionPoint;
    [SerializeField] private float _secondBetweenSpawn;

    private Spline _currentSpline;
    private WayTrace _currentWayTrace;
    private Vector3 _hieght = new Vector3(0, 0.13f, 0);
    private float _elapsedTime = 0;
    private bool _isSpawned = false;

    public Transform SpawnPoint => _spawnPoint;
    public Transform DirectionPoint => _directionPoint;

    protected override IEnumerator GoEffects()
    {
        while (TakeStatePlayer())
        {
            _spawnPoint.localPosition = Vector3.forward * (_setterSizeSnowball.NextSize / 2) + _hieght;
            _directionPoint.localPosition = Vector3.forward * (_setterSizeSnowball.NextSize / 2) + Vector3.forward * 0.05f + _hieght;

            if (_setterSizeSnowball.IsSnowball == false || _positionCheckerPlayer.IsOnPlate)
            {
                _isSpawned = false;
            }
            else if (_setterSizeSnowball.IsSnowball && _positionCheckerPlayer.IsOnGround)
            {
                if (_isSpawned == false)
                {
                    _currentWayTrace = Instantiate(_wayTrace, _spawnPoint.transform.position, _spawnPoint.transform.rotation, _container);
                    _currentSpline = _currentWayTrace.GetComponentInChildren<Spline>();
                    _isSpawned = true;
                }
                else if (_isSpawned)
                {
                    _elapsedTime += Time.deltaTime;
                    _currentWayTrace.TakePoints(this);

                    if (_elapsedTime > _secondBetweenSpawn)
                    {
                        _elapsedTime = 0;
                        _currentSpline.AddNode(new SplineNode(_currentWayTrace.PointSpawn.localPosition, _currentWayTrace.PointDirection.localPosition));
                    }
                }
            }

            yield return null;
        }
    }
}
