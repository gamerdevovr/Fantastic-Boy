using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isJumping = false;
    [SerializeField] private MobileLeftMove _mobileLeftMove;
    [SerializeField] private MobileRightMove _mobileRightMove;
    [SerializeField] private MobileJump _mobileJump;
    [SerializeField] private MobileSlidingMove _mobileSliding;

    private Rigidbody _rigidbody;
    private Vector3 _normal;
    private Animator _animator;
    private float _horizontal;



    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        _horizontal = _mobileLeftMove.GetLeftMove() + _mobileRightMove.GetRightMove();
        Vector3 direction = new Vector3(_horizontal, 0, 0);

        if (Vector3.Angle(Vector3.forward, direction) > 1f || Vector3.Angle(Vector3.forward, direction) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, direction, _runSpeed, 0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        Vector3 directionMove = direction.normalized - Vector3.Dot(direction.normalized, _normal) * _normal;
        Vector3 offset = directionMove * _runSpeed * Time.deltaTime;

        if (!_mobileSliding.Sliding())
        {
            _rigidbody.MovePosition(_rigidbody.position + offset);
            Debug.Log("Moving");
        }
    }

    private void Jump()
    {
        if (_mobileJump.Jump() && !_isJumping)
        {
            _isJumping = true;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpForce, _rigidbody.velocity.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Mathf.Abs(collision.contacts[0].normal.x) < 0.7f)
        {
            _normal = collision.contacts[0].normal;
        }
        
        _isJumping = false;
    }

    private void PlayAnimations()
    {
        if (_mobileSliding.Sliding())
        {
            Debug.Log(_mobileSliding.Sliding());
            ActiveAnimation("Sliding");
        }
        else if (_horizontal != 0)
        {
            if (_isJumping)
            {
                ActiveAnimation("Jumping");
            }
            else
            {
                ActiveAnimation("Running");
            }
        }
        else
        {
            if (_isJumping)
            {
                ActiveAnimation("JumpingFromIdle");
            }
            else
            {
                ActiveAnimation("Idle");
            }
        }
    }


    private void ActiveAnimation(string activAnimation)
    {
        for (int i = 0; i < _animator.parameterCount; i++)
        {
            if (activAnimation.Equals(_animator.GetParameter(i).name))
            {
                _animator.SetBool(_animator.GetParameter(i).name, true);
            }
            else 
            {
                _animator.SetBool(_animator.GetParameter(i).name, false);
            }
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
