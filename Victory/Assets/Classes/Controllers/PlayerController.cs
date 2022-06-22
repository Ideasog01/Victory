using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables & Properties

    public static Vector2 movementInput;
    public static Vector2 mousePosition;
    public static Transform playerTarget;
    public static bool disablePlayer;
    public static Vector3 playerCheckpoint;
    public static Interactable nearbyInteractable;
    public static List<EnemyController> nearbyEnemyList = new List<EnemyController>();
    public static EnemyController currentTarget;

    public PlayerData playerData;

    public enum Class { Archer, Knight, HolyWarrior };

    public Class playerClass;

    [Header("General Settings")]

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private int playerMaxHealth;

    [SerializeField]
    private float targetRadius;

    [SerializeField]
    private GameObject targetIndicator;

    [Header("Ability Settings")]

    [SerializeField]
    private float specialMaxCharge;

    [SerializeField]
    private float specialChargeRate;

    [SerializeField]
    private Ability ability1;

    [SerializeField]
    private Ability ability2;

    [SerializeField]
    private Ability primaryAbility;

    [SerializeField]
    private Ability secondaryAbility;

    private float _specialCharge;

    private int _playerHealth;

    private Quaternion _targetRotation;

    private Camera _gameCamera;

    private ArcherController _archerController;

    private GameManager _gameManager;

    private CharacterController _playerCharacterController;

    private int _targetIndex;
    private float _targetSwitchBuffer;

    public CharacterController PlayerCharacterController
    {
        get { return _playerCharacterController; }
    }

    public int PlayerMaxHealth
    {
        get { return playerMaxHealth; }
    }

    public int PlayerHealth
    {
        set { _playerHealth = value; }
        get { return _playerHealth; }
    }

    public Ability Ab1
    {
        get { return ability1; }
    }

    public Ability Ab2
    {
        get { return ability2; }
    }

    public Ability PrimaryAbility
    {
        get { return primaryAbility; }
    }

    public Ability SecondaryAbility
    {
        get { return secondaryAbility; }
    }

    #endregion

    #region Core

    private void Awake()
    {
        _gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _playerCharacterController = this.GetComponent<CharacterController>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (playerClass == Class.Archer)
        {
            _archerController = this.GetComponent<ArcherController>();
            _archerController.InitialiseArcher();
        }

        primaryAbility.RefreshAbility();
        secondaryAbility.RefreshAbility();
        ability1.RefreshAbility();
        ability2.RefreshAbility();
    }

    private void Start()
    {
        _playerHealth = playerMaxHealth;
        GameManager.playerInterface.DisplayPlayerHealth();
        GameManager.playerInterface.DisplayXP();
    }

    private void Update()
    {
        SpecialAbilityCharge();

        if(playerClass == Class.Archer)
        {
            _archerController.UpdateArcher();
        }

        FaceDirection();

        UpdateAbilityCooldowns();

        FindTargets();

        if(currentTarget != null)
        {
            targetIndicator.SetActive(true);
            targetIndicator.transform.position = currentTarget.transform.position + new Vector3(0, -0.75f, 0);
        }
        else
        {
            targetIndicator.SetActive(false);
        }

        if (!disablePlayer)
        {
            if (movementInput.x != 0 || movementInput.y != 0)
            {
                ApplyMovement();
            }
        }
    }

    public void TakeDamage(int amount)
    {
        _playerHealth -= amount;
        GameManager.playerInterface.DisplayPlayerHealth();

        if(_playerHealth <= 0)
        {
            disablePlayer = true;
            _gameManager.PlayerDefeat();
        }
    }

    public void AddExperience(int amount)
    {
        playerData.playerExperience += amount;

        GameManager.playerInterface.DisplayXP();
    }

    public void AddSpecialCharge(float amount)
    {
        _specialCharge += amount;

        if(_specialCharge > specialMaxCharge)
        {
            _specialCharge = specialMaxCharge;
        }
    }

    #endregion

    #region Inventory

    public void AddItem(Item item, int amount)
    {
        bool itemFound = false;
        int extraAmount = 0;

        foreach(Item inventoryItem in playerData.inventoryItems)
        {
            if(inventoryItem == item)
            {
                int currentAmount = playerData.inventoryAmounts[playerData.inventoryItems.IndexOf(inventoryItem)];
                
                if(currentAmount + amount < item.maxItemAmount)
                {
                    playerData.inventoryAmounts[playerData.inventoryItems.IndexOf(inventoryItem)] = currentAmount + amount;
                    itemFound = true;
                }
                else
                {
                    extraAmount = amount - item.maxItemAmount;
                    playerData.inventoryAmounts[playerData.inventoryItems.IndexOf(inventoryItem)] = item.maxItemAmount;
                }
            }
        }

        if(!itemFound)
        {
            if(playerData.inventoryItems.Count + 1 < playerData.maxItemAmount)
            {
                playerData.inventoryItems.Add(item);

                if(extraAmount == 0)
                {
                    playerData.inventoryAmounts.Add(amount);
                }
                else
                {
                    playerData.inventoryAmounts.Add(extraAmount);
                }
            }
        }
    }

    public void RemoveItem(Item item, int amount)
    {
        int extraAmount = 0;

        for(int i = 0; i < playerData.inventoryItems.Count; i++)
        {
            if (playerData.inventoryItems[i] == item)
            {
                if(amount == playerData.inventoryAmounts[i])
                {
                    playerData.inventoryItems.RemoveAt(i);
                    playerData.inventoryAmounts.RemoveAt(i);
                }
                else if(amount > playerData.inventoryAmounts[i])
                {
                    extraAmount = amount - playerData.inventoryAmounts[i];
                    playerData.inventoryItems.RemoveAt(i);
                    playerData.inventoryAmounts.RemoveAt(i);
                }
                else
                {
                    playerData.inventoryAmounts[i] -= amount;
                }
            }
        }

        if(extraAmount > 0)
        {
            RemoveItem(item, extraAmount);
        }
    }

    #endregion

    #region Interaction

    public void Primary()
    {
        if(!disablePlayer)
        {
            if (primaryAbility.abilityStatus == Ability.AbilityStatus.Available)
            {

            }

            RaycastHit hit;

            if (Physics.Raycast(_gameCamera.ScreenPointToRay(mousePosition), out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    playerTarget = hit.transform;
                }
            }
        }
    }

    public void PrimaryActivate()
    {
        if(!disablePlayer)
        {
            if (primaryAbility.abilityStatus == Ability.AbilityStatus.Available)
            {
                if (playerClass == Class.Archer)
                {
                    _archerController.ChargingArrow = true;
                }
            }
        }
    }

    public void PrimaryDeactivate()
    {
        if(!disablePlayer)
        {
            if (primaryAbility.abilityStatus == Ability.AbilityStatus.Available)
            {
                if (playerClass == Class.Archer)
                {
                    _archerController.ReleaseArrow();
                    _archerController.ChargingArrow = false;
                }
            }
        }
    }

    public void Secondary()
    {
        if(!disablePlayer)
        {
            if (secondaryAbility.abilityStatus == Ability.AbilityStatus.Available)
            {
                if (playerClass == Class.Archer)
                {
                    _archerController.SpawnDaggerProjectile();
                    secondaryAbility.UseAbility();
                }
            }
        }
    }

    public void Ability1()
    {
        if(!disablePlayer)
        {
            if (ability1.abilityStatus == Ability.AbilityStatus.Available)
            {
                if (playerClass == Class.Archer)
                {
                    this.GetComponent<ArcherController>().ExplosiveShotActive = true;
                }
            }
        }
    }

    public void Ability2()
    {
        if(!disablePlayer)
        {
            if (ability2.abilityStatus == Ability.AbilityStatus.Available)
            {
                if (playerClass == Class.Archer)
                {
                    this.GetComponent<ArcherController>().ActivateDash();
                    ability1.UseAbility();
                }
            }
        }
    }

    public void SpecialAbility()
    {
        if(!disablePlayer)
        {
            if (_specialCharge >= 25 && _specialCharge < 50)
            {
                if (playerClass == Class.Archer)
                {
                    _archerController.SuperiorReflex = 3;
                    _specialCharge = 0;
                }
            }

            if (_specialCharge >= 50 && _specialCharge < 75)
            {
                if (playerClass == Class.Archer)
                {
                    _archerController.PoisonShotActive = true;
                    _specialCharge = 0;
                }
            }

            if (_specialCharge >= 75)
            {
                if (playerClass == Class.Archer)
                {
                    _archerController.SpawnHunterTrap();
                    _specialCharge = 0;
                }
            }
        }
    }

    public void Interact()
    {
        if(!disablePlayer)
        {
            if(nearbyInteractable != null)
            {
                nearbyInteractable.Interact();
                nearbyInteractable = null;
                GameManager.playerInterface.DisplayInteractPrompt(false);
            }
        }
    }

    private void UpdateAbilityCooldowns()
    {
        if(ability1.abilityCooldown > 0 && !ability1.cooldownActive)
        {
            StartCoroutine(UpdateAbilityCooldowns(ability1));
        }

        if (ability2.abilityCooldown > 0 && !ability2.cooldownActive)
        {
            StartCoroutine(UpdateAbilityCooldowns(ability2));
        }

        if(primaryAbility.abilityCooldown > 0 && !primaryAbility.cooldownActive)
        {
            StartCoroutine(UpdateAbilityCooldowns(primaryAbility));
        }

        if (secondaryAbility.abilityCooldown > 0 && !secondaryAbility.cooldownActive)
        {
            StartCoroutine(UpdateAbilityCooldowns(secondaryAbility));
        }
    }

    private IEnumerator UpdateAbilityCooldowns(Ability ability)
    {
        ability.cooldownActive = true;
        yield return new WaitForSeconds(1);
        ability.abilityCooldown -= 1;
        ability.cooldownActive = false;

        if(ability.abilityCooldown <= 0)
        {
            ability.RefreshAbility();
        }
    }



    #endregion

    #region Ability

    public float MaxSpecialCharge
    {
        get { return specialMaxCharge; }
    }

    public void SpecialAbilityCharge()
    {
        if(_specialCharge < specialMaxCharge)
        {
            _specialCharge += specialChargeRate * Time.deltaTime;
        }

        GameManager.playerInterface.DisplaySpecialAbilityCharge(_specialCharge);
    }

    #endregion

    #region Movement

    private void FaceDirection()
    {
        if(!disablePlayer)
        {
            if(currentTarget != null)
            {
                _targetRotation = Quaternion.LookRotation(currentTarget.transform.position - this.transform.position);
            }
            else
            {
                if(movementInput.x != 0 || movementInput.y != 0)
                {
                    _targetRotation = Quaternion.LookRotation(new Vector3(movementInput.y, 0, -movementInput.x));
                }
            }

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, _targetRotation, Time.deltaTime * turnSpeed);
        }
    }

    public void AssignTarget()
    {
        if(!disablePlayer)
        {
            if(currentTarget == null)
            {
                if(nearbyEnemyList.Count > 0)
                {
                    _targetIndex = 0;
                    currentTarget = nearbyEnemyList[_targetIndex];
                }
            }
            else
            {
                currentTarget = null;
            }
        }
    }

    private void FindTargets()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(this.transform.position, targetRadius);

        foreach (Collider collider in enemyColliders)
        {
            if(collider.CompareTag("Enemy"))
            {
                if(!nearbyEnemyList.Contains(collider.GetComponent<EnemyController>()))
                {
                    nearbyEnemyList.Add(collider.GetComponent<EnemyController>());
                }
            }
        }
    }

    public void SwitchTarget()
    {
        if(!disablePlayer)
        {
            if (nearbyEnemyList.Count > 0)
            {
                if (_targetSwitchBuffer <= 0)
                {
                    _targetIndex++;

                    if (_targetIndex >= nearbyEnemyList.Count)
                    {
                        _targetIndex = 0;
                    }

                    currentTarget = nearbyEnemyList[_targetIndex];
                }
                else
                {
                    _targetSwitchBuffer -= Time.deltaTime * 1;
                }
            }
        }
        else
        {
            GameManager.dialogueManager.NextDialogue();
        }
    }

    private void ApplyMovement()
    {
        _playerCharacterController.Move(new Vector3(movementInput.y, 0, -movementInput.x) * Time.deltaTime * movementSpeed);
    }

    #endregion
}
