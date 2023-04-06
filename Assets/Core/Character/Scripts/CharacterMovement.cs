using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5;

    private CharacterAnimator _animator;
    private CharacterInput _input;

    private void Start()
    {
        _input = new CharacterInput();
        _animator = GetComponent<CharacterAnimator>();
    }

    private void Run(Vector3 direction)
    {
        Vector3 nextPosition = transform.position + (direction * _speed * Time.deltaTime);

        transform.LookAt(nextPosition);
        transform.position = nextPosition;
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