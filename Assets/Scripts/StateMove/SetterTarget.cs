using UnityEngine;
using UnityEngine.Events;

public abstract class SetterTarget : MonoBehaviour
{
    [SerializeField] private SetterSizeSnowball _setterSizeSnowball;
    [SerializeField] private Builder _builder;
    [SerializeField] private StopState _stop;
    [SerializeField] private MoveOnAxisZState _moveOnAxisZ;
    [SerializeField] private MoveOnSlideState _moveOnSlide;
    [SerializeField] private LayerMask _plate;
    [SerializeField] private Player _player;
    [SerializeField] protected PositionCheckerPlayer _positionCheckerPlayer;

    private bool _isMoveOnGround = false;
    private bool _isJump = false;

    public event UnityAction<PositionCheckerPlayer> StartedRun;
    public event UnityAction Stoped;

    private void Update()
    {
        StopCharacter();
        TryJump();

        if (_positionCheckerPlayer.IsOnPlate)
            MoveOnPlate();
        else if (_positionCheckerPlayer.IsOnStairs)
            MoveForward();
        else if (_positionCheckerPlayer.IsPlaceJump)
            Jump();
        else if (_positionCheckerPlayer.IsOnSlide)
            GoDownWithSlide();
        else if (CanGoOnGround())
            MoveOnGround();
    }

    protected abstract bool CanStop();

    protected abstract bool CanGoOnGround();

    protected abstract ITargetable TakeState();


    private void StopCharacter()
    {
        if (CanStop())
        {
            _player.MoveTo(_stop);
            Stoped?.Invoke();
            _isMoveOnGround = false;
        }
    }

    private void TryJump()
    {
        if (_isJump)
        {
            if (_positionCheckerPlayer.IsOnGround == false)
            {
                _moveOnAxisZ.GiveDirection(1);
                _player.MoveTo(_moveOnAxisZ);
            }
            else if (_positionCheckerPlayer.IsOnGround)
            {
                _isJump = false;
            }
        }
    }

    private void MoveOnPlate()
    {
        Stoped?.Invoke();

        if (_setterSizeSnowball.IsSnowball == false && _positionCheckerPlayer.GoCurrentBridge().CheckBuild() == false)
        {
            _moveOnAxisZ.GiveDirection(-1);
        }
        else if (_setterSizeSnowball.IsSnowball || _positionCheckerPlayer.GoCurrentBridge().CheckBuild())
        {
            _moveOnAxisZ.GiveDirection(1);
            _builder.TryBuild(_player, _plate);
        }

        _player.MoveTo(_moveOnAxisZ);
        _isMoveOnGround = false;
    }

    private void MoveForward()
    {
        _moveOnAxisZ.GiveDirection(1);
        _player.MoveTo(_moveOnAxisZ);
        _isMoveOnGround = false;
    }

    private void Jump()
    {
        if (_isJump == false)
        {
            _player.Jump();
            _isJump = true;
        }
    }

    private void GoDownWithSlide()
    {
        _player.MoveTo(_moveOnSlide);
        _isMoveOnGround = false;
    }

    private void MoveOnGround()
    {
        if (CanGoOnGround())
        {
            if (_isMoveOnGround == false)
            {
                StartedRun?.Invoke(_positionCheckerPlayer);
            }

            _player.MoveTo(TakeState());
            _isMoveOnGround = true;
        }
    }
}
