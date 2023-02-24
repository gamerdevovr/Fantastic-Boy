using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isJumping = false;
    [SerializeField] private MobileLeftMove _mobileLeftMove;
    [SerializeField] private MobileRightMove _mobileRightMove;
    [SerializeField] private MobileJump _mobileJump;

    private Rigidbody _rb;
    private Vector3 _normal;
    private Animator _animator;
    

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
        PlayAnimations();
    }

    private void Move()
    {
        float horizontal = _mobileLeftMove.GetLeftMove() + _mobileRightMove.GetLeftMove();
        //float horizontal = Input.GetAxis("Horizontal");



        Vector3 direction = new Vector3(horizontal, 0, 0);

        if (Vector3.Angle(Vector3.forward, direction) > 1f || Vector3.Angle(Vector3.forward, direction) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, direction, _runSpeed, 0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        Vector3 directionMove = direction.normalized - Vector3.Dot(direction.normalized, _normal) * _normal;
        Vector3 offset = directionMove * _runSpeed * Time.deltaTime;

        _rb.MovePosition(_rb.position + offset);
    }

    private void Jump()
    {
        if (_mobileJump.Jump() && !_isJumping)
        {
            _isJumping = true;
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.tag.Equals("Not Collision"))
        {
            _normal = collision.contacts[0].normal;
        }
        
        _isJumping = false;
    }

    private void PlayAnimations()
    {
        if (_mobileLeftMove.GetLeftMove() != 0 || _mobileRightMove.GetLeftMove() != 0)
        {
            _animator.SetBool("Running", true);
            _animator.SetBool("Idle", false);
            _animator.SetBool("Jump", false);
            if (_isJumping)
                _animator.SetBool("Jump", true);
        }
        else
        {
            _animator.SetBool("Idle", true);
            _animator.SetBool("Running", false);
            _animator.SetBool("Jump", false);
            if (_isJumping)
                _animator.SetBool("Jump", true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + _normal * 3);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * 2 )- Vector3.Dot(transform.forward, _normal) * _normal);
    }

}
