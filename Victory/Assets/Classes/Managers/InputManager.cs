using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private bool buildMode;

    private PlayerInput _playerInput;
    private PlayerController _playerController;
    private CivilianManager _civManager;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        if(!buildMode)
        {
            _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
            InitialiseExplorationInput();
        }
        else
        {
            _civManager = this.GetComponent<CivilianManager>();
            InitialiseBuildInput();
        }
    }

    private void Update()
    {
        if(!buildMode)
        {
            PlayerController.movementInput = _playerInput.ExplorationMode.MovementInput.ReadValue<Vector2>();
            PlayerController.mousePosition = _playerInput.ExplorationMode.MousePosition.ReadValue<Vector2>();
        }
        else
        {
            BuildManager.movementInput = _playerInput.BuildMode.MovementInput.ReadValue<Vector2>();
            BuildManager.mousePosition = _playerInput.BuildMode.MousePosition.ReadValue<Vector2>();
        }
    }

    private void InitialiseExplorationInput()
    {
        _playerInput.ExplorationMode.Primary.performed += ctx => _playerController.Primary();
        _playerInput.ExplorationMode.Secondary.performed += ctx => _playerController.Secondary();

        _playerInput.ExplorationMode.Primary.started += ctx => _playerController.PrimaryActivate();
        _playerInput.ExplorationMode.Primary.canceled += ctx => _playerController.PrimaryDeactivate();

        _playerInput.ExplorationMode.Jump.performed += ctx => _playerController.Jump();
        _playerInput.ExplorationMode.Interact.performed += ctx => _playerController.Interact();
        _playerInput.ExplorationMode.Ability1.performed += ctx => _playerController.Ability1();
        _playerInput.ExplorationMode.Ability2.performed += ctx => _playerController.Ability2();
        _playerInput.ExplorationMode.SpecialAbility.performed += ctx => _playerController.SpecialAbility();
    }

    private void InitialiseBuildInput()
    {
        // _playerInput.BuildMode.Primary.performed += ctx => _buildManager.PlaceObject();
        _playerInput.BuildMode.Primary.performed += ctx => _civManager.CivilianInteract();
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
