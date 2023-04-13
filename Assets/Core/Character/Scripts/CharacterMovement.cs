using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 5;
    [SerializeField] private Rigidbody _rigidbody;

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
        Vector3 velocity = direction * _speed;
        _rigidbody.velocity = velocity;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(velocity), Time.deltaTime * _rotationSpeed);
        }


        //Vector3 deltaPosition = direction * _speed * Time.deltaTime;
        //transform.position += _lastDeltaPosition = deltaPosition;
    }

    private void FixedUpdate()
    {
        Vector3 direction = _input.GetDirection();
        Run(direction);
        if (direction.magnitude > 0)
        {
            _animator.SetRunning();
        }
        else
        {
            _animator.SetIdle();
        }
    }
}