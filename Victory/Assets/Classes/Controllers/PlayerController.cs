using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    public static Vector2 movementInput;
    public static Vector2 mousePosition;
    public static Transform playerTarget;

    public enum Class { Archer, Knight, HolyWarrior };

    public Class playerClass;

    [Header("General Settings")]

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private LayerMask groundLayer;

    [Header("Jump Settings")]

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float maxJumpHeight;

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

    private Rigidbody _playerRb;
    private Transform _groundCheck;

    private bool _doubleJumpUsed;
    private bool _isJumping;

    private float _jumpHeight;
    private float _specialCharge;

    private Quaternion _targetRotation;

    private Camera _gameCamera;

    private ArcherController _archerController;

    private ResourceManager _resourceManager;

    public Rigidbody PlayerRigidBody
    {
        get { return _playerRb; }
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
        _playerRb = this.GetComponent<Rigidbody>();
        _groundCheck = this.transform.GetChild(1);

        _resourceManager = GameObject.Find("GameManager").GetComponent<ResourceManager>();

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

    private void FixedUpdate()
    {
        if(movementInput.x != 0 || movementInput.y != 0)
        {
            ApplyMovement();
        }

        FaceDirection();

        if (_isJumping)
        {
            if (this.transform.position.y > _jumpHeight)
            {
                _isJumping = false;
            }

            ApplyJumpForce();
        }
    }

    private void Update()
    {
        SpecialAbilityCharge();

        if(playerClass == Class.Archer)
        {
            _archerController.UpdateArcher();
        }

        UpdateAbilityCooldowns();
    }

    #endregion

    #region Interaction

    public void Primary()
    {
        if(primaryAbility.abilityStatus == Ability.AbilityStatus.Available)
        {
            
        }

        RaycastHit hit;

        if(Physics.Raycast(_gameCamera.ScreenPointToRay(mousePosition), out hit))
        {
            if(hit.collider.CompareTag("Enemy"))
            {
                playerTarget = hit.transform;
            }
        }
    }

    public void PrimaryActivate()
    {
        if(primaryAbility.abilityStatus == Ability.AbilityStatus.Available)
        {
            if (playerClass == Class.Archer)
            {
                _archerController.ChargingArrow = true;
            }
        }
    }

    public void PrimaryDeactivate()
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

    public void Secondary()
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

    public void Ability1()
    {
        if(ability1.abilityStatus == Ability.AbilityStatus.Available)
        {
            if (playerClass == Class.Archer)
            {
                this.GetComponent<ArcherController>().ActivateDash();
                ability1.UseAbility();
            }
        }
    }

    public void Ability2()
    {
        if(ability2.abilityStatus == Ability.AbilityStatus.Available)
        {
            if (playerClass == Class.Archer)
            {
                this.GetComponent<ArcherController>().ExplosiveShotActive = true;
            }
        }
    }

    public void SpecialAbility()
    {
        if(_specialCharge >= 25 && _specialCharge < 50)
        {
            if(playerClass == Class.Archer)
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
                _specialCharge = 0;
            }
        }
    }

    public void Interact()
    {
        _resourceManager.Interact();
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

    public void Jump()
    {
        if(IsGrounded())
        {
            _jumpHeight = this.transform.position.y + maxJumpHeight;
            _isJumping = true;
            _doubleJumpUsed = false;
        }
        else if(!_doubleJumpUsed)
        {
            _jumpHeight = this.transform.position.y + maxJumpHeight;
            _doubleJumpUsed = true;
            _isJumping = true;
        }
    }

    private void FaceDirection()
    {
        if(playerTarget != null)
        {
            Vector3 direction = playerTarget.position - this.transform.position;
            direction.y = 0;

            _targetRotation = Quaternion.LookRotation(direction);
        }
        else
        {
            if(movementInput.x != 0 || movementInput.y != 0)
            {
                Vector3 direction = new Vector3(movementInput.y, 0, -movementInput.x);
                direction.y = 0;

                _targetRotation = Quaternion.LookRotation(direction);
            }
        }

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, _targetRotation, Time.deltaTime * turnSpeed);
    }

    private void ApplyMovement()
    {
        _playerRb.MovePosition(transform.position + new Vector3(movementInput.y, 0, -movementInput.x) * Time.deltaTime * movementSpeed);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, 0.5f, groundLayer);
    }

    private void ApplyJumpForce()
    {
        if(!_doubleJumpUsed)
        {
            _playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            _playerRb.AddForce(Vector3.up * jumpForce * 1.2f, ForceMode.Impulse);
        }
    }

    #endregion
}
