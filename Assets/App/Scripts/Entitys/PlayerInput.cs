using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Action<Vector2> onMoveInput;

    [SerializeField] private InputAction position, press;

    [SerializeField] private float swipeResistance = 100;
    private Vector2 initialPos;
    private Vector2 currentPos => position.ReadValue<Vector2>();
    private void Awake()
    {
        position.Enable();
        press.Enable();
        press.performed += _ => { initialPos = currentPos; };
        press.canceled += _ => DetectSwipe();
    }

    private void DetectSwipe()
    {
        Vector2 delta = currentPos - initialPos;
        Vector2 direction = Vector2.zero;

        if (Mathf.Abs(delta.x) > swipeResistance)
        {
            direction.x = Mathf.Clamp(delta.x, -1, 1);
        }
        if (Mathf.Abs(delta.y) > swipeResistance)
        {
            direction.y = Mathf.Clamp(delta.y, -1, 1);
        }

        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) direction.y = 0;
        else direction.x = 0;

        if(Mathf.Abs(direction.x) == Math.Abs(direction.y)) direction = new Vector2(direction.x, 0);

        if (direction != Vector2.zero)
            onMoveInput?.Invoke(direction);
    }
}