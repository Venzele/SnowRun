using UnityEngine;

public class MoveOnSlideState : MonoBehaviour, ITargetable
{
    [SerializeField] private Transform _player;

    private int _offsetZ = 1;
    private float _offsetY = -0.2f;

    public Vector3 IndicatePoint()
    {
        return new Vector3(_player.position.x, _player.position.y + _offsetY, _player.position.z + _offsetZ);
    }
}
