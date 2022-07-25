using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetterTarget : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TrackingState _tracking;
    [SerializeField] private StopState _stop;
    [SerializeField] private MovingForvardState _movingForvard;
    [SerializeField] private SpawnerSnowball _spawnerSnowball;
    [SerializeField] private LayerMask _bridge;
    [SerializeField] private LayerMask _ground;

    private Bridge _currentBridge;

    public event UnityAction<Player> StartedRun;
    public event UnityAction Stoped;
    public event UnityAction WentOnBridge;

    public bool IsBridge => Physics.CheckSphere(_player.transform.position, 0.5f, _bridge);
    public bool IsGround => Physics.CheckSphere(_player.transform.position, 0.5f, _ground);

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartedRun?.Invoke(_player);
        }

        if (Input.GetMouseButton(0) && IsGround)
        {
            _player.MoveTo(_tracking);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _player.MoveTo(_stop);
            Stoped?.Invoke();
        }

        if (IsBridge && _spawnerSnowball.IsSpawn)
        {
            Collider[] Bridges = Physics.OverlapSphere(_player.transform.position, 0.5f, _bridge);
            _player.MoveTo(_movingForvard);

            if (Bridges.Length > 0)
            {
                _currentBridge =  Bridges[0].GetComponent<Bridge>();

                if (_currentBridge.IsBuilt == false)
                {
                    WentOnBridge?.Invoke();
                    _currentBridge.Build();
                }
            }
        } 
    }
}
