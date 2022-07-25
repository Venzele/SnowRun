using UnityEngine;

public class MovingForvardState : MonoBehaviour, ITargetable
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _bridge;

    private readonly int _offsetZ = 1;

    public Vector3 IndicatePoint()
    {
        return new Vector3(_bridge.position.x, _player.position.y, _player.position.z + _offsetZ);
    }
}
