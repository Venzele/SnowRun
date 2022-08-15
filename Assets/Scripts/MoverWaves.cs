using UnityEngine;

public class MoverWaves : MonoBehaviour
{
    [SerializeField] private Transform _waves;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _pointOfRerurn;
    [SerializeField] private float _speed;

    private void Update()
    {
        _waves.position = Vector3.MoveTowards(_waves.position, _target.position, Time.deltaTime * _speed);

        if (_waves.position == _target.position)
            _waves.position = _pointOfRerurn.position;
    }
}
