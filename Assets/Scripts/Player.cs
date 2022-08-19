using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private int _jumpForce;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private LayerMask _bridge;
    [SerializeField] private LayerMask _stairs;
    [SerializeField] private LayerMask _slide;
    [SerializeField] private LayerMask _plate;
    [SerializeField] private LayerMask _placeJump;
    [SerializeField] private bool _isBot;

    private Vector3 _lastPosition;
    private Vector3 _currentPostion;

    public float LengthHands { get; private set; }
    public bool IsOnBridge => Physics.CheckSphere(transform.position, 0.5f, _bridge);
    public bool IsOnSlide => Physics.CheckSphere(transform.position, 0.5f, _slide);
    public bool IsOnStairs => Physics.CheckSphere(transform.position, 0.5f, _stairs);
    public bool IsOnPlate => Physics.CheckSphere(transform.position, 0.5f, _plate);
    public bool IsOnGround => Physics.CheckSphere(transform.position, 0.5f, _ground);
    public bool IsPlaceJump => Physics.CheckSphere(transform.position, 0.5f, _placeJump);
    public bool IsRun => _lastPosition != _currentPostion;
    public bool IsBot => _isBot;

    private void Start()
    {
        LengthHands = transform.localScale.x / 3;
    }

    private void Update()
    {
        _lastPosition = _currentPostion;
        _currentPostion = transform.position;
    }

    public void MoveTo(ITargetable target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.IndicatePoint(), Time.deltaTime * _speed);
        transform.LookAt(target.IndicatePoint());
    }

    public void Accelerate()
    {
        _speed = 9;
    }

    public void AccelerateMore()
    {
        _speed = 17;
    }

    public void AccelerateInJump()
    {
        _speed = 52;
    }

    public void Slow()
    {
        _speed = 7;
    }

    public void Jump()
    {
        _rigidbody.velocity = Vector3.up * _jumpForce;
    }

    public Bridge GoCurrentBridge()
    {
        if (IsOnPlate)
        {
            Collider[] plates = Physics.OverlapSphere(transform.position, 0.5f, _plate);
            return plates[0].GetComponentInParent<Bridge>();
        }
        else if (IsOnBridge)
        {
            Collider[] bridge = Physics.OverlapSphere(transform.position, 0.5f, _bridge);
            return bridge[0].GetComponent<Bridge>();
        }

        return null;
    }
}
