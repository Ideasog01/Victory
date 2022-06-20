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

    private List<ProjectileController> _projectilePrefabList = new List<ProjectileController>();

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
        bool projectileFound = false;

        foreach(ProjectileController projectile in _projectilePrefabList)
        {
            if(!projectile.isActiveAndEnabled)
            {
                projectile.gameObject.SetActive(true);
                projectile.transform.position = _arrowSpawn.position;
                projectile.transform.localEulerAngles = this.transform.localEulerAngles;
                projectile.InitialiseProjectile();
                projectileFound = true;
                break;
            }
        }

        if(!projectileFound)
        {
            ProjectileController projectileController = Instantiate(arrowPrefab.GetComponent<ProjectileController>(), _arrowSpawn.position, Quaternion.identity);
            _projectilePrefabList.Add(projectileController);
        }
    }
}
