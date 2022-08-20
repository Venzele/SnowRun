using UnityEngine;
using UnityEngine.Events;

public class SetterTarget : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SetterSizeSnowball _setterSizeSnowball;
    [SerializeField] private Builder _builder;
    [SerializeField] private StopState _stop;
    [SerializeField] private MoveOnAxisZState _moveOnAxisZ;
    [SerializeField] private MoveOnSlideState _moveOnSlide;
    [SerializeField] private LayerMask _plate;

    private bool _isMoveOnGround = false;
    private bool _isJump = false;

    public event UnityAction<Player> StartedRun;
    public event UnityAction Stoped;

    private void Update()
    {
        StopCharacter();
        TryJump();

        if (_player.IsOnPlate)
            MoveOnPlate();
        else if (_player.IsOnStairs)
            MoveForward();
        else if (_player.IsPlaceJump)
            Jump();
        else if (_player.IsOnSlide)
            GoDownWithSlide();
        else if (CanGoOnGround(_player))
            MoveOnGround();
    }

    protected virtual bool CanStop(Player player)
    {
        return false;
    }

    protected virtual bool CanGoOnGround(Player player)
    {
        return false;
    }

    protected virtual ITargetable TakeState()
    {
        return null;
    }

    private void StopCharacter()
    {
        if (CanStop(_player))
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
    }

    private void MoveOnPlate()
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
        if (CanGoOnGround(_player))
        {
            if (_isMoveOnGround == false)
                StartedRun?.Invoke(_player);

            _player.MoveTo(TakeState());
            _isMoveOnGround = true;
        }
    }
}
