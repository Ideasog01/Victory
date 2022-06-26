using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool controllerConnected;

    [SerializeField]
    private bool buildMode;

    private PlayerInput _playerInput;
    private PlayerController _playerController;
    private InventoryInterface _inventoryInterface;
    private CivilianManager _civManager;
    private GameManager _gameManager;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        if(!buildMode)
        {
            _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
            _gameManager = this.GetComponent<GameManager>();
            _inventoryInterface = this.GetComponent<InventoryInterface>();
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
        _playerInput.ExplorationMode.Secondary.performed += ctx => _playerController.Secondary();

        _playerInput.ExplorationMode.Pause.performed += ctx => _gameManager.PauseGame();

        _playerInput.ExplorationMode.SwitchTarget.performed += ctx => _playerController.SwitchTarget();
        _playerInput.ExplorationMode.LockOn.performed += ctx => _playerController.AssignTarget();

        _playerInput.ExplorationMode.Inventory.performed += ctx => _inventoryInterface.DisplayInventory();

        _playerInput.ExplorationMode.Primary.started += ctx => _playerController.PrimaryActivate();
        _playerInput.ExplorationMode.Primary.canceled += ctx => _playerController.PrimaryDeactivate();

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
