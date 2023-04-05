using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterInput _input;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _rotationSpeed = 5;

    private Vector3 _lastDeltaPos; 
    
    private CharacterStates State
    {
        get => (CharacterStates)_animator.GetInteger("state");
        set => _animator.SetInteger("state", (int)value);
    }

    private void Run(Vector3 direction)
    {
        State = CharacterStates.Run;
        
        Vector3 deltaPos = direction * _speed * Time.deltaTime;

        transform.position += deltaPos;
        
        if (deltaPos.magnitude > 0)
        {
            _lastDeltaPos = deltaPos;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_lastDeltaPos), Time.deltaTime * _rotationSpeed);
    }

    private void Idle()
    {
        State = CharacterStates.Idle;
    }

    private void Update()
    {
        Vector3 direction = _input.GetDirection();
        direction.z = direction.y;
        direction.y = 0;

        if (direction.magnitude > 0)
        {
            Run(direction);
        }
        else
        {
            Idle();
        }
    }
}

enum CharacterStates
{
    Idle,
    Run
}