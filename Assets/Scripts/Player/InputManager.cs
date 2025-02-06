using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.PlayerOnfootActions onFoot;

    private PlayerMotor motor;
    private MouseMovement look;
    void Awake()
    {
        playerInput =  new PlayerInput();
        onFoot = playerInput.PlayerOnfoot;
        look = GetComponent<MouseMovement>();
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<MouseMovement>();

        onFoot.Jump.performed += ctx => motor.Jump();

        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Move.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
