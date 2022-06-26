using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Weapon assignedWeapon;

    [SerializeField]
    private string compareTag;

    private bool _isWeaponActive;

    private float _weaponCharge = 1;

    private float _attackTimer;

    private Transform _projectileSpawn;

    private bool _isWeaponCharging;

    public bool IsWeaponActive
    {
        set { _isWeaponActive = value; }
        get { return _isWeaponActive; }
    }
    
    public bool WeaponCharging
    {
        set {_isWeaponCharging = value;}
    }

    public Weapon AssignedWeapon
    {
        set {assignedWeapon = value;}
    }

    private void Awake()
    {
        //Set Projectile Spawn
        _projectileSpawn = this.transform.GetChild(0).GetChild(0);
    }

    private void Update()
    {
        if(_attackTimer > 0)
        {
            _attackTimer -= Time.deltaTime * 1;
        }

        if(_isWeaponCharging)
        {
            ChargeHold();
        }
    }

    public void WeaponDeactivation()
    {
         _isWeaponActive = false;
    }

    public void Attack()
    {
        if(assignedWeapon.isMelee)
        {
            this.GetComponent<Animator>().SetTrigger("attack");
            _isWeaponActive = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isWeaponActive)
        {
            if(other.CompareTag(compareTag))
            {
                if (compareTag == "Player")
                {
                    other.GetComponent<PlayerController>().TakeDamage(assignedWeapon.weaponDamage);
                    _isWeaponActive = false;
                    Debug.Log("Player took damage!");
                }

                if (compareTag == "Enemy")
                {
                    other.GetComponent<EnemyController>().TakeDamage(assignedWeapon.weaponDamage);
                    _isWeaponActive = false;
                }
            }
        }
    }

    private void SpawnProjectile()
    {
        GameManager.projectileManager.SpawnProjectile(assignedWeapon.projectilePrefab, _projectileSpawn.position, this.transform.parent.localEulerAngles, assignedWeapon.projectileSpeed * _weaponCharge, assignedWeapon.weaponDamage * (int)_weaponCharge);
    }

    public void ChargeRelease()
    {
        if (_weaponCharge > 1)
        {
            SpawnProjectile();
            _weaponCharge = 1;
            GameManager.playerInterface.DisplayArrowCharge(_weaponCharge, assignedWeapon.maxWeaponCharge);
            _isWeaponCharging = false;
        }
    }

    public void ChargeHold()
    {
        if (_weaponCharge < assignedWeapon.maxWeaponCharge)
        {
            _weaponCharge += assignedWeapon.weaponChargeRate * Time.deltaTime;
            GameManager.playerInterface.DisplayArrowCharge(_weaponCharge, assignedWeapon.maxWeaponCharge);
        }
    }
}
