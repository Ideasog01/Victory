using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool controllerConnected;
    private PlayerInput _playerInput;
    private PlayerController _playerController;
    private InventoryInterface _inventoryInterface;
    private GameManager _gameManager;
    private ClassInterface _classInterface;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _gameManager = this.GetComponent<GameManager>();
        _classInterface = _gameManager.GetComponent<ClassInterface>();
        _inventoryInterface = this.GetComponent<InventoryInterface>();
        InitialiseExplorationInput();

    }

    private void Update()
    {
        PlayerController.movementInput = _playerInput.Gameplay.MovementInput.ReadValue<Vector2>();
        PlayerController.mousePosition = _playerInput.Gameplay.MousePosition.ReadValue<Vector2>();
    }

    private void InitialiseExplorationInput()
    {
        _playerInput.Gameplay.Pause.performed += ctx => _gameManager.PauseGame();

        _playerInput.Gameplay.Secondary.performed += ctx => _playerController.MouseInteract();

        _playerInput.Gameplay.Inventory.performed += ctx => _inventoryInterface.DisplayInventory();
        _playerInput.Gameplay.Primary.canceled += ctx => _classInterface.EquipEnhancement();
        _playerInput.Gameplay.Primary.started += ctx => _classInterface.SelectEnhancementOption();

        _playerInput.Gameplay.Primary.started += ctx => _playerController.PrimaryActivate();
        _playerInput.Gameplay.Primary.canceled += ctx => _playerController.PrimaryDeactivate();

        _playerInput.Gameplay.Interact.performed += ctx => _playerController.Interact();
        _playerInput.Gameplay.Ability1.performed += ctx => _playerController.Ability1();
        _playerInput.Gameplay.Ability2.performed += ctx => _playerController.Ability2();
        _playerInput.Gameplay.Ability3.performed += ctx => _playerController.Ability3();
        _playerInput.Gameplay.Ability4.performed += ctx => _playerController.Ability4();
        _playerInput.Gameplay.SpecialAbility.performed += ctx => _playerController.SpecialAbility();
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
