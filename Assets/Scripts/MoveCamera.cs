using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 0.05f;
    [SerializeField] private Transform _playerTransform;

    private Vector3 _offset;

    private void Awake()
    {
        _offset = transform.position - _playerTransform.position;
    }

    private void FixedUpdate()
    {
        Vector3 newCamPosition = _playerTransform.position + _offset;

        transform.position = Vector3.Lerp(transform.position, newCamPosition, _moveSpeed);
    }
}
