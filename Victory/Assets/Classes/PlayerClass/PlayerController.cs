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
    public static bool disablePlayerMovement;
    public static Vector3 playerCheckpoint;
    public static Interactable nearbyInteractable;
    public static List<EnemyController> nearbyEnemyList = new List<EnemyController>();
    public static EnemyController currentTarget;
    public static Weapon playerWeapon;

    public enum Class { Archer, VoidServant };

    public Class playerClass;

    [Header("Ability Settings")]

    public Ability ability1;

    public Ability ability2;

    public Ability ability3;

    public Ability ability4;

    public Ability specialAbility1;

    public Ability specialAbility2;

    public Ability specialAbility3;

    [SerializeField]
    private float specialMaxCharge;

    [SerializeField]
    private float specialChargeRate;


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

    private float _specialCharge;

    private int _playerHealth;

    private Quaternion _targetRotation;

    private Camera _gameCamera;

    private ArcherController _archerController;

    private GameManager _gameManager;

    private CharacterController _playerCharacterController;

    private PlayerInterface _playerInterface;

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

    #endregion

    #region Core

    private void Awake()
    {
        _gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _playerCharacterController = this.GetComponent<CharacterController>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _playerInterface = GameObject.Find("GameManager").GetComponent<PlayerInterface>();

        _playerData = GameObject.Find("GlobalManager").GetComponent<GlobalManager>().playerData;

        if (playerClass == Class.Archer)
        {
            _archerController = this.GetComponent<ArcherController>();
        }

        ability1.RefreshAbility();
        ability2.RefreshAbility();
        ability3.RefreshAbility();
        ability4.RefreshAbility();
        specialAbility1.RefreshAbility();
        specialAbility2.RefreshAbility();
        specialAbility3.RefreshAbility();
        playerWeapon = null;

        disablePlayer = false;
        disablePlayerMovement = false;

        DisplayAbilityIcons();
    }

    private void Start()
    {
        playerMaxHealth *= _playerData.playerLevel;
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

        if(!disablePlayer)
        {
            if(movementInput.x != 0 || movementInput.y != 0)
            {
                ApplyMovement();
            }


        }
    }

    public void TeleportPlayer(Vector3 position)
    {
        _playerCharacterController.enabled = false;
        this.transform.position = position;
        StartCoroutine(WaitForTeleport());
    }

    private IEnumerator WaitForTeleport()
    {
        yield return new WaitForSeconds(0.1f);
        _playerCharacterController.enabled = true;
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
    }

    #endregion

    #region Interaction

    public void MouseInteract()
    {
        RaycastHit hit;
        if(Physics.Raycast(_gameCamera.ScreenPointToRay(mousePosition), out hit))
        {
            if(hit.collider.CompareTag("Enemy"))
            {
                currentTarget = hit.collider.GetComponent<EnemyController>();
                Debug.Log("Enemy Found!");
            }
            else if(currentTarget != null)
            {
                currentTarget = null;
                Debug.Log("Enemy no longer target");
            }
        }
    }

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

    public void Ability1()
    {
        if(!disablePlayer)
        {
            if (ability1.abilityStatus == Ability.AbilityStatus.Available)
            {
                if(playerClass == Class.VoidServant)
                {
                    this.GetComponent<VoidServantController>().VoidBolt();
                }

                ability1.ActivateAbility();
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
                }

                if(playerClass == Class.VoidServant)
                {
                    this.GetComponent<VoidServantController>().EmptyAbyss();
                }    

                ability2.ActivateAbility();
            }
        }
    }

    public void Ability3()
    {
        if(!disablePlayer)
        {
            if(ability3.abilityStatus == Ability.AbilityStatus.Available)
            {
                if(playerClass == Class.VoidServant)
                {
                    this.GetComponent<VoidServantController>().TemporalBlink();
                }    

                ability3.ActivateAbility();
            }
            else
            {
                if (playerClass == Class.VoidServant)
                {
                    this.GetComponent<VoidServantController>().ActivateTemportalBlink();
                }
            }
        }
    }

    public void Ability4()
    {
        if(!disablePlayer)
        {
            if(ability4.abilityStatus == Ability.AbilityStatus.Available)
            {
                if(playerClass == Class.VoidServant)
                {
                    this.GetComponent<VoidServantController>().LifeForceGrab();
                }    

                ability4.ActivateAbility();
            }
        }
    }

    public void SpecialAbility()
    {
        if(!disablePlayer && specialAbility1.abilityStatus == Ability.AbilityStatus.Available && specialAbility2.abilityStatus == Ability.AbilityStatus.Available && specialAbility3.abilityStatus == Ability.AbilityStatus.Available)
        {

            if (_specialCharge >= 25 && _specialCharge < 50)
            {
                if (playerClass == Class.Archer)
                {
                    
                }

                if(playerClass == Class.VoidServant)
                {
                    this.GetComponent<VoidServantController>().ChaoticBloom();
                }

                specialAbility1.ActivateAbility();
            }

            if (_specialCharge >= 50 && _specialCharge < 75)
            {
                if (playerClass == Class.Archer)
                {
                    
                }

                if (playerClass == Class.VoidServant)
                {
                    this.GetComponent<VoidServantController>().SuppresiveBeam();
                }

                specialAbility2.ActivateAbility();
            }

            if (_specialCharge >= 75)
            {
                if (playerClass == Class.Archer)
                {
                    _archerController.SpawnHunterTrap();
                }

                if (playerClass == Class.VoidServant)
                {
                    this.GetComponent<VoidServantController>().VoidMonster();
                }

                specialAbility3.ActivateAbility();
            }

            if(_specialCharge >= 25)
            {
                _specialCharge = 0;
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

        if (ability3.abilityCooldown > 0 && !ability3.cooldownActive)
        {
            StartCoroutine(UpdateAbilityCooldowns(ability3));
        }

        if (ability4.abilityCooldown > 0 && !ability4.cooldownActive)
        {
            StartCoroutine(UpdateAbilityCooldowns(ability4));
        }

        if (specialAbility1.abilityCooldown > 0 && !specialAbility1.cooldownActive)
        {
            StartCoroutine(UpdateAbilityCooldowns(specialAbility1));
        }

        if (specialAbility2.abilityCooldown > 0 && !specialAbility2.cooldownActive)
        {
            StartCoroutine(UpdateAbilityCooldowns(specialAbility2));
        }

        if (specialAbility3.abilityCooldown > 0 && !specialAbility3.cooldownActive)
        {
            StartCoroutine(UpdateAbilityCooldowns(specialAbility3));
        }

        //Active Cooldowns

        if (ability1.activeTimer > 0 && !ability1.activeTimerActive)
        {
            StartCoroutine(UpdateAbilityActiveTimer(ability1));
        }

        if (ability2.activeTimer > 0 && !ability2.activeTimerActive)
        {
            StartCoroutine(UpdateAbilityActiveTimer(ability2));
        }

        if (ability3.activeTimer > 0 && !ability3.activeTimerActive)
        {
            StartCoroutine(UpdateAbilityActiveTimer(ability3));
        }

        if (ability4.activeTimer > 0 && !ability4.activeTimerActive)
        {
            StartCoroutine(UpdateAbilityActiveTimer(ability4));
        }

        if (specialAbility1.activeTimer > 0 && !specialAbility1.activeTimerActive)
        {
            StartCoroutine(UpdateAbilityActiveTimer(specialAbility1));
        }

        if (specialAbility2.activeTimer > 0 && !specialAbility2.activeTimerActive)
        {
            StartCoroutine(UpdateAbilityActiveTimer(specialAbility2));
        }

        if (specialAbility3.activeTimer > 0 && !specialAbility3.activeTimerActive)
        {
            StartCoroutine(UpdateAbilityActiveTimer(specialAbility3));
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

    private IEnumerator UpdateAbilityActiveTimer(Ability ability)
    {
        ability.activeTimerActive = true;
        yield return new WaitForSeconds(1);
        ability.activeTimer -= 1;
        ability.activeTimerActive = false;

        if(ability.activeTimer <= 0)
        {
            ability.UseAbility();

            if(ability == ability1)
            {
                Ability1End();
            }
            else if(ability == ability2)
            {
                Ability2End();
            }
            else if(ability == ability3)
            {
                Ability3End();
            }
            else if(ability == ability4)
            {
                Ability4End();
            }
            else if(ability == specialAbility1)
            {
                SpecialAbility1End();
            }
            else if(ability == specialAbility2)
            {
                SpecialAbility2End();
            }
            else if(ability == specialAbility3)
            {
                SpecialAbility3End();
            }
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

    public void Ability1End()
    {
        
    }

    public void Ability2End()
    {

    }

    public void Ability3End()
    {
        if(playerClass == Class.VoidServant)
        {
            this.GetComponent<VoidServantController>().EndSupressiveBeam();
        }
    }

    public void Ability4End()
    {

    }

    public void SpecialAbility1End()
    {

    }

    public void SpecialAbility2End()
    {
        
    }

    public void SpecialAbility3End()
    {

    }

    private void DisplayAbilityIcons()
    {
        _playerInterface.Ability1Icon.sprite = ability1.abilityIcon;
        _playerInterface.Ability2Icon.sprite = ability2.abilityIcon;
        _playerInterface.Ability3Icon.sprite = ability3.abilityIcon;
        _playerInterface.Ability4Icon.sprite = ability4.abilityIcon;
    }

    #endregion

    #region Movement

    private void FaceDirection()
    {
        if(!disablePlayer)
        {
            if(currentTarget == null)
            {
                RaycastHit hit;

                if(Physics.Raycast(_gameCamera.ScreenPointToRay(mousePosition), out hit))
                {
                    Vector3 direction = hit.point - this.transform.position;
                    direction.y = 0;
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    _targetRotation = rotation;
                }
            }
            else
            {
                Vector3 direction = currentTarget.transform.position - this.transform.position;
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction);
                _targetRotation = rotation;
            }

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, _targetRotation, Time.deltaTime * turnSpeed);
            this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
            
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
        if(!disablePlayerMovement)
        {
            if(_playerCharacterController.enabled)
            {
                _playerCharacterController.Move(new Vector3(movementInput.y, 0, -movementInput.x) * Time.deltaTime * movementSpeed);
            }
        }
    }

    #endregion
}
