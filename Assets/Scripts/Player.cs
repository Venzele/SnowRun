using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private int _speed;

    public float HalfScaleSize { get; private set; }

    private void Start()
    {
        HalfScaleSize = transform.localScale.x / 2;
    }

    public void MoveTo(ITargetable target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.IndicatePoint(), Time.deltaTime * _speed);
        transform.LookAt(target.IndicatePoint());
    }
}
