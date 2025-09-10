using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _moveInput;

    [SerializeField]
    private float _movementSpeed;

    void Update()
    {
        // move player
        Vector2 moveInput = _moveInput * _movementSpeed;
        transform.position += (Vector3)moveInput;
    }

    // input system fields
    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }
}
