using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Action<Vector2> onMoveInput;

    public void MoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            onMoveInput?.Invoke(context.ReadValue<Vector2>());
        }
    }
}