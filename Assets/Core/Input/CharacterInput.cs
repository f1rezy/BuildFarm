using UnityEngine;

public class CharacterInput : ICharacterInput
{
    private GameInput _gameInput;

    public CharacterInput()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();
    }

    public Vector3 GetDirection()
    {
        var inputDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
        var direction = new Vector3(inputDirection.x, 0f, inputDirection.y);
        return direction;
    }
}