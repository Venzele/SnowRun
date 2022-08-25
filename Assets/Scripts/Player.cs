using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private int _jumpForce;

    private int _speed;
    private int _speedOnGround = 7;
    private int _speedOnPlate = 9;
    private int _speedOnSlide = 17;
    private int _speedInJump = 52;

    public float LengthHands { get; private set; }

    private void Start()
    {
        LengthHands = transform.localScale.x / 3;
        _speed = _speedOnGround;
    }

    public void MoveTo(ITargetable target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.IndicatePoint(), Time.deltaTime * _speed);
        transform.LookAt(target.IndicatePoint());
    }

    public void Accelerate()
    {
        _speed = _speedOnPlate;
    }

    public void AccelerateMore()
    {
        _speed = _speedOnSlide;
    }

    public void AccelerateInJump()
    {
        _speed = _speedInJump;
    }

    public void Slow()
    {
        _speed = _speedOnGround;
    }

    public void Jump()
    {
        _rigidbody.velocity = Vector3.up * _jumpForce;
    }
}