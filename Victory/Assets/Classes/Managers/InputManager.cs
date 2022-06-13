using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _playerInput = new PlayerInput();
        InitialiseInput();
    }

    private void Update()
    {
        PlayerController.movementInput = _playerInput.Gameplay.MovementInput.ReadValue<Vector2>();
    }

    private void InitialiseInput()
    {
        _playerInput.Gameplay.Jump.performed += ctx => _playerController.Jump();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
