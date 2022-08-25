using UnityEngine;

public class MoveOnAxisZState : MonoBehaviour, ITargetable
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _pointAxisX;

    private int _offsetZ = 1;

    public Vector3 IndicatePoint()
    {
        return new Vector3(_pointAxisX.position.x, _player.position.y, _player.position.z + _offsetZ);
    }

    public int GiveDirection(int direction)
    {
        return _offsetZ = direction;
    }
}
