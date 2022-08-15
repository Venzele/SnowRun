using System.Collections.Generic;
using UnityEngine;

public class WayBotState : MonoBehaviour, ITargetable
{
    [SerializeField] private Transform _bot;
    [SerializeField] private List<Vector3> _way;

    private int _indexPoint;
    private int _quantityPath;

    private void Start()
    {
        _indexPoint = 0;
        _quantityPath = 0;
    }

    public Vector3 IndicatePoint()
    {
        if (_way[_indexPoint] == _bot.position)
            ChangePoint();

        if (_quantityPath == 2)
            return _bot.position;

        return _way[_indexPoint];
    }

    private void ChangePoint()
    {
        if (_indexPoint >= _way.Count - 1)
        {
            _indexPoint = 0;
            _quantityPath++;
        }
        else
        {
            _indexPoint++;
        }
    }
}
