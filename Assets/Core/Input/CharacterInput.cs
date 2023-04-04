using UnityEngine;

public class CharacterInput : MonoBehaviour, ICharacterInput
{
    private GameInput _gameInput;
    private Vector3 _inputDirection;

    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();
    }

    private void Update()
    {
        _inputDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
    }

    public Vector3 GetDirection()
    {
        return _inputDirection;
    }
}