using UnityEngine;

public class AnimatorCharacter : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SetterSizeSnowball _setterSizeSnowball;
    [SerializeField] private Player _player;
    [SerializeField] private Material _currentColor;

    private const string Idle = "Idle";
    private const string Run = "Run";
    private const string Push = "Push";
    private const string Sit = "Sit";
    private const string Jump = "Jump";
    private const string Back = "Back";
    private const string Dance = "Dance";
    private const string Red = "Red";
    private const string Purple = "Purple";

    private void Update()
    {
        if (_player.IsOnGround)
        {
            PlayOnGround();
        }
        else if (_player.IsOnStairs)
        {
            PlayOnStairs();
        }
        else if (_player.IsOnBridge)
        {
            _player.Accelerate();

            if (_setterSizeSnowball.IsSnowball == false && _player.GoCurrentBridge().CheckBuild() == false)
                TurnOnPlayBack();
            else
                TurnOnPlayForward();
        }
        else if (_player.IsPlaceJump)
        {
            PlayJump();
        }
        else if (_player.IsOnSlide)
        {
            PlayOnSlide();
        }
    }

    private void PlayOnGround()
    {
        _player.Slow();

        if (_player.IsRun == false)
        {
            _animator.Play(Idle);
        }
        else if (_player.IsRun)
        {
            if (_setterSizeSnowball.IsSnowball == false)
                _animator.Play(Run);
            else if (_setterSizeSnowball.IsSnowball == true)
                _animator.Play(Push);
        }
    }

    private void PlayOnStairs()
    {
        _player.Accelerate();

        if (_setterSizeSnowball.IsSnowball == false)
            _animator.Play(Run);
        else
            _animator.Play(Push);
    }

    private void TurnOnPlayBack()
    {
        if (_currentColor.name == Red)
            _animator.Play(Back);
        else if (_currentColor.name == Purple)
            _animator.Play(Dance);
    }

    private void TurnOnPlayForward()
    {
        if (_currentColor.name == Red)
            _animator.Play(Sit);
        else if (_currentColor.name == Purple)
            _animator.Play(Dance);
    }

    private void PlayJump()
    {
        _player.AccelerateInJump();
        _animator.Play(Jump);
    }

    private void PlayOnSlide()
    {
        _player.AccelerateMore();
        _animator.Play(Sit);
    }
}
