using UnityEngine;
using UnityEngine.Events;

public class PositionCheckerPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;
    [SerializeField] private LayerMask _bridge;
    [SerializeField] private LayerMask _stairs;
    [SerializeField] private LayerMask _slide;
    [SerializeField] private LayerMask _plate;
    [SerializeField] private LayerMask _placeJump;

    private Vector3 _lastPosition;
    private Vector3 _currentPostion;

    public event UnityAction ReachedGround;
    public event UnityAction ReachedPlate;
    public event UnityAction ReachedBridge;
    public event UnityAction ReachedStairs;
    public event UnityAction ReachedSlide;
    public event UnityAction ReachedPlaceJump;

    public bool IsOnBridge => Physics.CheckSphere(transform.position, 0.5f, _bridge);
    public bool IsOnSlide => Physics.CheckSphere(transform.position, 0.5f, _slide);
    public bool IsOnStairs => Physics.CheckSphere(transform.position, 0.5f, _stairs);
    public bool IsOnPlate => Physics.CheckSphere(transform.position, 0.5f, _plate);
    public bool IsOnGround => Physics.CheckSphere(transform.position, 0.5f, _ground);
    public bool IsPlaceJump => Physics.CheckSphere(transform.position, 0.5f, _placeJump);
    public bool IsRun => _lastPosition != _currentPostion;

    private void Awake()
    {
        _lastPosition = _currentPostion;
        _currentPostion = transform.position;
    }

    private void Update()
    {
        _lastPosition = _currentPostion;
        _currentPostion = transform.position;
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

    private void OnCollisionEnter(Collision collision)
    {
        var plate = collision.gameObject.GetComponent<Plate>();
        var ground = collision.gameObject.GetComponent<Ground>();
        var stairs = collision.gameObject.GetComponent<Stairs>();
        var bridge = collision.gameObject.GetComponent<Bridge>();
        var placeJump = collision.gameObject.GetComponent<PlaceJump>();
        var slide = collision.gameObject.GetComponent<Slide>();

        if (ground)
            ReachedGround?.Invoke();
        else if (plate)
            ReachedPlate?.Invoke();
        else if (stairs)
            ReachedStairs?.Invoke();
        else if (bridge)
            ReachedBridge?.Invoke();
        else if (placeJump)
            ReachedPlaceJump?.Invoke();
        else if (slide)
            ReachedSlide?.Invoke();
    }
}
