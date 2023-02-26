using UnityEngine;

public class Enemy : MonoBehaviour
{
    static public Enemy Instance;

    [SerializeField] private int _health  { get; set; }
    [SerializeField] private int _attack = 5;
    [SerializeField] private float _runSpeed = 2f;
    [SerializeField] private bool _enableMovement = false;

    private Rigidbody _rigidbody;
    private Vector3 _normal;
    private Vector3 _direction = new Vector3(-1f, 0f, 0f);

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move()
    {
        if (_enableMovement)
        {
            if (Vector3.Angle(Vector3.forward, _direction) > 1f || Vector3.Angle(Vector3.forward, _direction) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, _direction, _runSpeed, 0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }

            Vector3 directionMove = _direction.normalized - Vector3.Dot(_direction.normalized, _normal) * _normal;
            Vector3 offset = directionMove * _runSpeed * Time.deltaTime;

            _rigidbody.MovePosition(_rigidbody.position + offset);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Mathf.Abs(collision.contacts[0].normal.x) < 0.7f)
        {
            _normal = collision.contacts[0].normal;
        }

        if (collision.gameObject.tag.Equals("Wall"))
        {
            _direction.x *= -1f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + _normal * 3);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * 2) - Vector3.Dot(transform.forward, _normal) * _normal);
    }

}
