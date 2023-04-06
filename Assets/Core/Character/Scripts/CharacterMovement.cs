using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _rotationSpeed = 5;

    private CharacterAnimator _animator;
    private CharacterInput _input;

    private Vector3 _lastDeltaPosition;

    private void Start()
    {
        _input = new CharacterInput();
        _animator = GetComponent<CharacterAnimator>();
    }

    private void Run(Vector3 direction)
    {
        Vector3 deltaPosition = direction * _speed * Time.deltaTime;

        transform.position += _lastDeltaPosition = deltaPosition;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_lastDeltaPosition), Time.deltaTime * _rotationSpeed);
    }

    private void FixedUpdate()
    {
        Vector3 direction = _input.GetDirection();
        if (direction.magnitude > 0)
        {
            Run(direction);
            _animator.SetRunning();
        }
        else
        {
            _animator.SetIdle();
        }
    }
}