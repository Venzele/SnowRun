using UnityEngine;

public class AnimatorCharacter : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SetterSizeSnowball _setterSizeSnowball;
    [SerializeField] private Player _player;
    [SerializeField] private SetterTarget _setterTarget;
    [SerializeField] private PositionCheckerPlayer _positionCheckerPlayer;
    [SerializeField] private Material _currentColor;

    private const string NumberAnimation = "NumberAnimation";
    private const int Idle = 1;
    private const int Run = 2;
    private const int Push = 3;
    private const int Sit = 4;
    private const int Jump = 5;
    private const int Back = 6;
    private const int Dance = 7;
    private const string Red = "Red";
    private const string Purple = "Purple";

    private void OnEnable()
    {
        _setterTarget.Stoped += PlayIdle;
        _positionCheckerPlayer.ReachedGround += PlayOnGround;
        _positionCheckerPlayer.ReachedStairs += PlayOnStairs;
        _positionCheckerPlayer.ReachedBridge += PlayOnBridge;
        _positionCheckerPlayer.ReachedPlaceJump += PlayJump;
        _positionCheckerPlayer.ReachedSlide += PlayOnSlide;
    }

    private void OnDisable()
    {
        _setterTarget.Stoped -= PlayIdle;
        _positionCheckerPlayer.ReachedGround -= PlayOnGround;
        _positionCheckerPlayer.ReachedStairs -= PlayOnStairs;
        _positionCheckerPlayer.ReachedBridge -= PlayOnBridge;
        _positionCheckerPlayer.ReachedPlaceJump -= PlayJump;
        _positionCheckerPlayer.ReachedSlide -= PlayOnSlide;
    }

    //private void Update()
    //{
    //    if (_positionCheckerPlayer.IsOnGround)
    //    {
    //        PlayOnGround();
    //    }
    //    else if (_positionCheckerPlayer.IsOnStairs)
    //    {
    //        PlayOnStairs();
    //    }
    //    else if (_positionCheckerPlayer.IsOnBridge)
    //    {
    //        PlayOnBridge();
    //    }
    //    else if (_positionCheckerPlayer.IsPlaceJump)
    //    {
    //        PlayJump();
    //    }
    //    else if (_positionCheckerPlayer.IsOnSlide)
    //    {
    //        PlayOnSlide();
    //    }
    //}

    private void PlayIdle()
    {
        _animator.SetInteger(NumberAnimation, Idle);
    }

    private void PlayOnGround()
    {
        //_player.Slow();

        if (_positionCheckerPlayer.IsRun == false)
        {
            _animator.SetInteger(NumberAnimation, Idle);
        }
        else if (_positionCheckerPlayer.IsRun)
        {
            if (_setterSizeSnowball.IsSnowball == false)
                _animator.SetInteger(NumberAnimation, Run);
            else if (_setterSizeSnowball.IsSnowball == true)
                _animator.SetInteger(NumberAnimation, Push);
        }
    }

    private void PlayOnStairs()
    {
        //_player.Accelerate();

        if (_setterSizeSnowball.IsSnowball == false)
            _animator.SetInteger(NumberAnimation, Run);
        else
            _animator.SetInteger(NumberAnimation, Push);
    }

    private void PlayOnBridge()
    {
        //_player.Accelerate();

        if (_setterSizeSnowball.IsSnowball == false && _positionCheckerPlayer.GoCurrentBridge().CheckBuild() == false)
            TurnOnPlayBack();
        else
            TurnOnPlayForward();
    }

    private void TurnOnPlayBack()
    {
        if (_currentColor.name == Red)
            _animator.SetInteger(NumberAnimation, Back);
        else if (_currentColor.name == Purple)
            _animator.SetInteger(NumberAnimation, Dance);
    }

    private void TurnOnPlayForward()
    {
        if (_currentColor.name == Red)
            _animator.SetInteger(NumberAnimation, Sit);
        else if (_currentColor.name == Purple)
            _animator.SetInteger(NumberAnimation, Dance);
    }

    private void PlayJump()
    {
        //_player.AccelerateInJump();
        _animator.SetInteger(NumberAnimation, Jump);
    }

    private void PlayOnSlide()
    {
        //_player.AccelerateMore();
        _animator.SetInteger(NumberAnimation, Sit);
    }
}
