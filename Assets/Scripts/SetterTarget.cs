using UnityEngine;
using UnityEngine.Events;

public class SetterTarget : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SetterSizeSnowball _setterSizeSnowball;
    [SerializeField] private Builder _builder;
    [SerializeField] private TrackingState _tracking;
    [SerializeField] private StopState _stop;
    [SerializeField] private WayBotState _wayBot;
    [SerializeField] private MoveOnAxisZState _moveOnAxisZ;
    [SerializeField] private MoveOnSlideState _moveOnSlide;
    [SerializeField] private LayerMask _plate;

    private bool _isMoveOnGround = false;
    private bool _isJump = false;

    public event UnityAction<Player> StartedRun;
    public event UnityAction Stoped;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _player.MoveTo(_stop);
            Stoped?.Invoke();
            _isMoveOnGround = false;
        }
        else if (_isJump)
        {
            if (_player.IsOnGround == false)
            {
                _moveOnAxisZ.GiveDirection(1);
                _player.MoveTo(_moveOnAxisZ);
            }
            else if (_player.IsOnGround)
            {
                _isJump = false;
            }
        }

        if (_player.IsOnPlate)
        {
            Stoped?.Invoke();

            if (_setterSizeSnowball.IsSnowball == false && _player.GoCurrentBridge().CheckBuild() == false)
            {
                _moveOnAxisZ.GiveDirection(-1);
            }
            else if (_setterSizeSnowball.IsSnowball || _player.GoCurrentBridge().CheckBuild())
            {
                _moveOnAxisZ.GiveDirection(1);
                _builder.TryBuild(_player, _plate);
            }

            _player.MoveTo(_moveOnAxisZ);
            _isMoveOnGround = false;
        }
        else if (_player.IsOnStairs)
        {
            _moveOnAxisZ.GiveDirection(1);
            _player.MoveTo(_moveOnAxisZ);
            _isMoveOnGround = false;
        }
        else if (_player.IsPlaceJump)
        {
            if (_isJump == false)
            {
                _player.Jump();
                _isJump = true;
            }
        }
        else if (_player.IsOnSlide)
        {
            _player.MoveTo(_moveOnSlide);
            _isMoveOnGround = false;
        }
        else if (Input.GetMouseButton(0) && _player.IsOnGround && _player.IsBot == false)
        {
            if (_isMoveOnGround == false)
                StartedRun?.Invoke(_player);

            _player.MoveTo(_tracking);
            _isMoveOnGround = true;
        }
        else if (_player.IsOnGround && _player.IsBot)
        {
            if (_isMoveOnGround == false)
                StartedRun?.Invoke(_player);

            _player.MoveTo(_wayBot);
            _isMoveOnGround = true;
        }
    }
}
