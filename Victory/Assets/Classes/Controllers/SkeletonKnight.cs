using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonKnight : EnemyController
{
    [SerializeField]
    private WeaponController swordWeapon;

    [SerializeField]
    private float attackCooldown;

    private float _attackTimer;

    private void Update()
    {
        EnemyAIStateMachine();
        EnemyBehaviour();
    }

    public void ActivateWeapon()
    {
        if(swordWeapon.IsWeaponActive)
        {
            swordWeapon.IsWeaponActive = false;
        }
        else
        {
            swordWeapon.IsWeaponActive = true;
        }
    }

    private void EnemyBehaviour()
    {
        if(EnemyState == AIState.Chase)
        {
            SetEnemyDestination(Player.transform.position);
        }
        else if(EnemyState == AIState.Idle)
        {
            if(!PatrolLocationSet)
            {
                if (PatrolLocations.Length > 0)
                {
                    PatrolIndex = Random.Range(0, PatrolLocations.Length);
                    SetEnemyDestination(PatrolLocations[PatrolIndex]);
                }
            }
            else
            {
                if(this.transform.position == EnemyDestination)
                {
                    PatrolLocationSet = false;
                }
            }
        }
        else if(EnemyState == AIState.Attack)
        {
            if(_attackTimer <= 0)
            {
                this.GetComponent<Animator>().SetTrigger("attack");
                swordWeapon.IsWeaponActive = true;
                _attackTimer = attackCooldown;
            }
            else
            {
                _attackTimer -= Time.deltaTime * 1;
            }
        }
    }
}
