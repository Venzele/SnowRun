using UnityEngine;

public class StopState : MonoBehaviour, ITargetable
{
    [SerializeField] private Transform _point;

    public Vector3 IndicatePoint()
    {
        return _point.position;
    }
}
