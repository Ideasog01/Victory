using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : PlayerController
{
    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private Transform hunterTrap;

    [Header("Arrow Settings")]

    [SerializeField]
    private float maxArrowCharge;

    [SerializeField]
    private float arrowChargeRate;

    [SerializeField]
    private float maxArrowHoldTime;

    [SerializeField]
    private Vector3 baseProjectileSpeed;

    [SerializeField]
    private int baseProjectileDamage;

    [Header("Strafe Settings")]

    [SerializeField]
    private float dashSpeed;

    [SerializeField]
    private float dashDuration;

    private Transform _projectileSpawn;

    private bool _chargingArrow;

    private float _arrowCharge = 1;
    private float _arrowHoldTime;
    private float _dashTimer;

    private bool _explosiveShotActive;
    private int _superiorReflexActive;
    private bool _poisonShotActive;

    public bool ChargingArrow
    {
        set { _chargingArrow = value; }
    }

    public float MaxArrowCharge
    {
        get { return maxArrowCharge; }
    }

    public bool ExplosiveShotActive
    {
        set { _explosiveShotActive = value; }
    }

    public bool PoisonShotActive
    {
        set { _poisonShotActive = value; }
    }

    public int SuperiorReflex
    {
        set { _superiorReflexActive = value; }
    }

    public void InitialiseArcher()
    {
        _projectileSpawn = this.transform.GetChild(2);
    }

    public void UpdateArcher()
    {
        if(_chargingArrow)
        {
            ChargeArrow();
        }

        if (_dashTimer > 0)
        {
            _dashTimer -= Time.deltaTime * 1;
            PlayerCharacterController.Move(new Vector3(movementInput.y, 0, -movementInput.x) * Time.deltaTime * dashSpeed);
        }
    }

    public void ActivateDash()
    {
        _dashTimer = dashDuration;
    }

    public void SpawnDaggerProjectile()
    {
        GameManager.projectileManager.SpawnDaggerProjectile(_projectileSpawn.position, this.transform.localEulerAngles);
    }

    public void SpawnHunterTrap()
    {
        Instantiate(hunterTrap, this.transform.position + new Vector3(0, -0.5f, 0), this.transform.rotation);
    }

    private void SpawnArrowProjectile()
    {
        if(!_explosiveShotActive && !_poisonShotActive)
        {
            Vector3 projectileSpeed = baseProjectileSpeed * _arrowCharge;
            GameManager.projectileManager.SpawnArrowProjectile(_projectileSpawn.position, this.transform.localEulerAngles, projectileSpeed, baseProjectileDamage * (int)_arrowCharge);
        }
        else if(_poisonShotActive)
        {
            Vector3 projectileSpeed = baseProjectileSpeed * _arrowCharge;
            GameManager.projectileManager.SpawnPoisonArrowProjectile(_projectileSpawn.position, this.transform.localEulerAngles, projectileSpeed, baseProjectileDamage * (int)_arrowCharge);
            _poisonShotActive = false;
        }
        else if(_explosiveShotActive)
        {
            Vector3 projectileSpeed = baseProjectileSpeed * _arrowCharge;
            GameManager.projectileManager.SpawnExplosiveArrowProjectile(_projectileSpawn.position, this.transform.localEulerAngles, projectileSpeed, baseProjectileDamage * (int)_arrowCharge);
            _explosiveShotActive = false;
            Ab2.UseAbility();
        }
    }

    public void ReleaseArrow()
    {
        if (_arrowCharge > 1)
        {
            SpawnArrowProjectile();
            _arrowCharge = 1;
            GameManager.playerInterface.DisplayArrowCharge(_arrowCharge);
            Debug.Log("Arrow Fired with an arrow charge speed of " + _arrowCharge);
        }
    }

    public void ChargeArrow()
    {
        if (_arrowCharge < maxArrowCharge)
        {
            if(_superiorReflexActive > 0)
            {
                _arrowCharge = maxArrowCharge;
                GameManager.playerInterface.DisplayArrowCharge(_arrowCharge);
                _superiorReflexActive--;
            }
            else
            {
                _arrowCharge += arrowChargeRate * Time.deltaTime;
                GameManager.playerInterface.DisplayArrowCharge(_arrowCharge);
            }
        }
        else
        {
            if (_arrowHoldTime < maxArrowHoldTime)
            {
                _arrowHoldTime += Time.deltaTime * 1;
            }
            else
            {
                ReleaseArrow();
                _arrowCharge = 0;
                _arrowHoldTime = 0;
            }
        }
    }
}
