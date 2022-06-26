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
    public static Weapon playerWeapon;

    public static SpecialItem playerSpecialItem;

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

    [SerializeField]
    private Transform equipedWeapon;

    [SerializeField]
    private WeaponController weaponController;

    [Header("Ability Settings")]

    [SerializeField]
    private float specialMaxCharge;

    [SerializeField]
    private float specialChargeRate;

    [SerializeField]
    private Ability ability1;

    [SerializeField]
    private Ability ability2;

    private float _specialCharge;

    private int _playerHealth;

    private Quaternion _targetRotation;

    private Camera _gameCamera;

    private ArcherController _archerController;

    private GameManager _gameManager;

    private CharacterController _playerCharacterController;

    private int _targetIndex;
    private float _targetSwitchBuffer;
    private PlayerData _playerData;

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

    #endregion

    #region Core

    private void Awake()
    {
        _gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _playerCharacterController = this.GetComponent<CharacterController>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _playerData = GameObject.Find("GlobalManager").GetComponent<GlobalManager>().playerData;

        if (playerClass == Class.Archer)
        {
            _archerController = this.GetComponent<ArcherController>();
        }

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

        FaceDirection();

        UpdateAbilityCooldowns();

        FindTargets();

        if(playerClass == Class.Archer)
        {
            _archerController.UpdateArcher();
        }

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

    public void Heal(int amount)
    {
        _playerHealth += amount;
        GameManager.playerInterface.DisplayPlayerHealth();

        if(_playerHealth > playerMaxHealth)
        {
            _playerHealth = playerMaxHealth;
        }
        
    }

    public void AddExperience(int amount)
    {
        _playerData.playerExperience += amount;

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

        foreach(Item inventoryItem in _playerData.inventoryItems)
        {
            if(inventoryItem == item)
            {
                int currentAmount = _playerData.inventoryAmounts[_playerData.inventoryItems.IndexOf(inventoryItem)];
                
                if(currentAmount + amount < item.maxItemAmount)
                {
                    _playerData.inventoryAmounts[_playerData.inventoryItems.IndexOf(inventoryItem)] = currentAmount + amount;
                    itemFound = true;
                }
                else
                {
                    extraAmount = amount - item.maxItemAmount;
                    _playerData.inventoryAmounts[_playerData.inventoryItems.IndexOf(inventoryItem)] = item.maxItemAmount;
                }
            }
        }

        if(!itemFound)
        {
            if(_playerData.inventoryItems.Count + 1 < _playerData.maxItemAmount)
            {
                _playerData.inventoryItems.Add(item);

                if(extraAmount == 0)
                {
                    _playerData.inventoryAmounts.Add(amount);
                }
                else
                {
                    _playerData.inventoryAmounts.Add(extraAmount);
                }
            }
        }
    }

    public void RemoveItem(Item item, int amount)
    {
        int extraAmount = 0;

        for(int i = 0; i < _playerData.inventoryItems.Count; i++)
        {
            if (_playerData.inventoryItems[i] == item)
            {
                if(amount == _playerData.inventoryAmounts[i])
                {
                    _playerData.inventoryItems.RemoveAt(i);
                    _playerData.inventoryAmounts.RemoveAt(i);
                }
                else if(amount > _playerData.inventoryAmounts[i])
                {
                    extraAmount = amount - _playerData.inventoryAmounts[i];
                    _playerData.inventoryItems.RemoveAt(i);
                    _playerData.inventoryAmounts.RemoveAt(i);
                }
                else
                {
                    _playerData.inventoryAmounts[i] -= amount;
                }
            }
        }

        if(extraAmount > 0)
        {
            RemoveItem(item, extraAmount);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        //Drop Previous Weapon

        if(playerWeapon != null)
        {
            GameManager.spawnManager.SpawnWeaponDrop(playerWeapon, this.transform.position);
        }
        
        //Set Weapon
        equipedWeapon.gameObject.SetActive(true);
        weaponController.AssignedWeapon = weapon;
        playerWeapon = weapon;
        equipedWeapon.GetChild(0).GetComponent<MeshFilter>().mesh = weapon.weaponMesh;
        equipedWeapon.GetChild(0).GetComponent<MeshRenderer>().material = weapon.weaponMaterial;
        equipedWeapon.GetChild(0).localPosition = weapon.weaponOffset;
        equipedWeapon.GetChild(0).transform.localScale = weapon.weaponScale;
        equipedWeapon.GetComponent<BoxCollider>().size = weapon.weaponColliderSize;
        equipedWeapon.GetComponent<BoxCollider>().center = weapon.weaponColliderOffset;

        //Change Primary Icon Display
        GameManager.playerInterface.PrimaryIcon.sprite = weapon.weaponIcon;
    }

    #endregion

    #region Interaction

    public void PrimaryActivate()
    {
        if(!disablePlayer)
        {
            if(playerWeapon != null)
            {
                if (playerWeapon.weaponName == "Bow")
                {
                    weaponController.WeaponCharging = true;
                }
            }
        }
    }

    public void PrimaryDeactivate()
    {
        if(!disablePlayer)
        {
            if(playerWeapon != null)
            {
                if (playerWeapon.weaponName == "Bow")
                {
                    weaponController.ChargeRelease();
                }

                if(playerWeapon.isMelee)
                {
                    weaponController.Attack();
                }
            }
        }
    }

    public void Secondary()
    {
        if(!disablePlayer)
        {
            if(playerSpecialItem != null)
            {
                Instantiate(playerSpecialItem.itemPrefab, this.transform.position, Quaternion.identity);
                playerSpecialItem = null;
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
                    ability2.UseAbility();
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
                    _specialCharge = 0;
                }
            }

            if (_specialCharge >= 50 && _specialCharge < 75)
            {
                if (playerClass == Class.Archer)
                {
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
