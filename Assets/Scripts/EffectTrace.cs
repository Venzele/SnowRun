using SplineMesh;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectTrace : SetterEffects
{
    [SerializeField] private Spline _spline;


    //[SerializeField] private Transform _container;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _directionPoint;
    //[SerializeField] private GameObject _template;
    //[SerializeField] private int _capacity;
    [SerializeField] private float _secondBetweenSpawn;

    //private List<GameObject> _pool = new List<GameObject>();
    //private Queue<GameObject> _activeTrace = new Queue<GameObject>();
    private Spline _currentSpline;
    private Vector3 _hieght = new Vector3(0, 0.2f, 0);
    //private float _offsetZ = 0.5f;
    private float _elapsedTime = 0;
    private bool _isSpawned = false;

    //private void Start()
    //{
    //    Initialize();
    //}

    protected override bool PlayEffect(Player player)
    {
        return Input.GetMouseButton(0) && player.IsOnGround;
    }

    protected override IEnumerator DropSnowflakes(SetterSizeSnowball setterSizeSnowball, Player player)
    {
        //_spawnPoint.localPosition = Vector3.forward * (setterSizeSnowball.NextSize / 2) + _hieght;
        //_spline.TakeFirstNode(_spawnPoint.position);
        //_directionPoint.localPosition = Vector3.forward * (setterSizeSnowball.NextSize / 2) + Vector3.forward * _offsetZ + _hieght;
        //_spline.TakeFirstDirection(_directionPoint.position);
        //_spline.TakeSecondNode(_directionPoint.position);
        //_directionPoint.localPosition = Vector3.forward * (setterSizeSnowball.NextSize / 2) + Vector3.forward + _hieght;
        //_spline.TakeSecondDirection(_directionPoint.position);

        while (PlayEffect(player))
        {
            _spawnPoint.localPosition = Vector3.forward * (setterSizeSnowball.NextSize / 2) + _hieght;
            _directionPoint.localPosition = Vector3.forward * (setterSizeSnowball.NextSize / 2) + Vector3.forward * 0.05f + _hieght;

            if (setterSizeSnowball.IsSnowball)
            {
                if (_isSpawned == false)
                {
                    _currentSpline = Instantiate(_spline, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
                    _isSpawned = true;
                }
                else if (_isSpawned)
                {
                    _elapsedTime += Time.deltaTime;

                    if (_elapsedTime > _secondBetweenSpawn)
                    {
                        _elapsedTime = 0;
                        _currentSpline.AddNode(new SplineNode(_spawnPoint.position, _directionPoint.position));
                    }
                }
            }
            else if (setterSizeSnowball.IsSnowball == false)
            {
                _isSpawned = false;
            }

            yield return null;
        }
    }

    //protected override IEnumerator DropSnowflakes(SetterSizeSnowball setterSizeSnowball, Player player)
    //{
    //    while (PlayEffect(player))
    //    {
    //        _elapsedTime += Time.deltaTime;

    //        if (_elapsedTime > _secondBetweenSpawn)
    //        {
    //            if (TryGetObject(out GameObject trace))
    //            {
    //                _elapsedTime = 0;
    //                _spawnPoint.localPosition = Vector3.forward * (setterSizeSnowball.NextSize / 2) + _hieght;
    //                Quaternion spawnRotation = transform.rotation;
    //                trace.SetActive(true);
    //                trace.transform.position = _spawnPoint.position;
    //                trace.transform.rotation = spawnRotation;
    //                _activeTrace.Enqueue(trace);
    //            }
    //        }

    //        yield return null;
    //    }
    //}

    //private bool TryGetObject(out GameObject result)
    //{
    //    result = _pool.FirstOrDefault(p => p.activeSelf == false);
    //    return result != null;
    //}

    //private void Initialize()
    //{
    //    for (int i = 0; i < _capacity; i++)
    //    {
    //        GameObject spawned = Instantiate(_template, _container);
    //        spawned.SetActive(false);
    //        _pool.Add(spawned);
    //    }
    //}
}
