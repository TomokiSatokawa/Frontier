using PlayerInput;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _rotationSpeed;
    private Vector3 _velocity;
    public float MoveAmount { get; private set; }

    public void Update()
    {
        float moveSpeed = InputManager.Dash > 0f ? _dashSpeed : _moveSpeed;

        Vector3 cameraInput = GetCameraDirection(InputManager.Move);

        RotateTowards(cameraInput, _rotationSpeed);
        _velocity = cameraInput * moveSpeed;

        float inputMagnitude = InputManager.Move.magnitude;
        MoveAmount = inputMagnitude + (inputMagnitude > 0f ? InputManager.Dash : 0);
        _rigidbody.linearVelocity = _velocity;
    }

    private Vector3 GetCameraDirection(Vector2 input)
    {
        Vector3 forward = _camera.forward;
        Vector3 right = _camera.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return (forward * input.y) + (right * input.x);
    }

    private void RotateTowards(Vector3 direction, float rotateSpeed)
    {
        if (direction.sqrMagnitude <= 0f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards( transform.rotation,    targetRotation, rotateSpeed * Time.deltaTime);
    }
}
