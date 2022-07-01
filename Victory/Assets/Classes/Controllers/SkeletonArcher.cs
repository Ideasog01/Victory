using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcher : EnemyController
{
    [SerializeField]
    private float attackCooldown;

    [SerializeField]
    private Transform arrowPrefab;

    private float _attackTimer;

    private Transform _arrowSpawn;

    private void Start()
    {
        _arrowSpawn = this.transform.GetChild(2);
    }

    private void Update()
    {
        EnemyAIStateMachine();
        EnemyBehaviour();
    }

    private void EnemyBehaviour()
    {
        if (EnemyState == AIState.Chase)
        {
            SetEnemyDestination(Player.transform.position);
        }
        else if (EnemyState == AIState.Idle)
        {
            if (!PatrolLocationSet)
            {
                if (PatrolLocations.Length > 0)
                {
                    PatrolIndex = Random.Range(0, PatrolLocations.Length);
                    SetEnemyDestination(PatrolLocations[PatrolIndex]);
                }
            }
            else
            {
                if (this.transform.position == EnemyDestination)
                {
                    PatrolLocationSet = false;
                }
            }
        }
        else if (EnemyState == AIState.Attack)
        {
            if (_attackTimer <= 0)
            {
                SpawnArcherArrow();
                _attackTimer = attackCooldown;
            }
            else
            {
                _attackTimer -= Time.deltaTime * 1;
            }
        }
    }

    public void SpawnArcherArrow()
    {
        ProjectileController projectileController = Instantiate(arrowPrefab.GetComponent<ProjectileController>(), _arrowSpawn.position, this.transform.rotation);
        projectileController.InitialiseProjectile();
    }
}
