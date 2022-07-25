using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Transform _target;

    private readonly int _offsetY = 9;
    private readonly int _offsetZ = -8;

    private void Update()
    {
        _mainCamera.transform.position = new Vector3(_target.position.x, _target.position.y + _offsetY, _target.position.z + _offsetZ);
    }
}
