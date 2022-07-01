using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VoidMonster : MonoBehaviour
{
    [SerializeField]
    private Transform voidMonsterEye;

    [SerializeField]
    private float rotationSpeed = 180;

    [SerializeField]
    private float attackRadius;

    [SerializeField]
    private float projectileSpawnTime;

    [SerializeField]
    private Transform projectileSpawn;

    [SerializeField]
    private Transform projectilePrefab;

    [SerializeField]
    private float distanceThreshold;

    [SerializeField]
    private Transform emptyAbyssPrefab;

    [SerializeField]
    private float chargeSpeed = 15;

    [SerializeField]
    private float chargeDuration;

    [SerializeField]
    private int chargeDamage;

    private List<EnemyController> _nearbyEnemies = new List<EnemyController>();

    private Transform _monsterTarget;

    private float _projectileSpawnTimer;

    private NavMeshAgent _navMeshAgent;
    private PlayerController _playerController;

    private float _missileCooldown;
    private float _abyssCooldown;
    private bool _chargeActivated;
    private bool _isCharging;

    private void Awake()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(!_chargeActivated)
        {
            if (_monsterTarget != null)
            {
                Vector3 direction = _monsterTarget.position - voidMonsterEye.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                voidMonsterEye.rotation = Quaternion.Lerp(voidMonsterEye.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                if (_projectileSpawnTimer > 0)
                {
                    _projectileSpawnTimer -= Time.deltaTime * 1;
                }
                else
                {
                    SpawnProjectile();
                    _projectileSpawnTimer = projectileSpawnTime;
                }

                float distanceToTarget = Vector3.Distance(this.transform.position, _monsterTarget.position);

                if (distanceToTarget > distanceThreshold)
                {
                    _navMeshAgent.SetDestination(_monsterTarget.position);
                }
                else
                {
                    _navMeshAgent.SetDestination(this.transform.position);
                    _isCharging = false;
                }
            }
            else
            {
                Quaternion targetRotation = Quaternion.LookRotation(this.transform.forward);
                voidMonsterEye.rotation = Quaternion.Lerp(voidMonsterEye.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                EnemyController enemy = NearbyEnemy();

                if (enemy != null)
                {
                    _monsterTarget = enemy.transform;
                }

                float distanceToPlayer = Vector3.Distance(this.transform.position, _playerController.transform.position);

                if (distanceToPlayer > distanceThreshold)
                {
                    _navMeshAgent.SetDestination(_playerController.transform.position);
                }
                else
                {
                    _navMeshAgent.SetDestination(this.transform.position);
                    _isCharging = false;
                }
            }

            if (_missileCooldown > 0)
            {
                _missileCooldown -= Time.deltaTime * 1;
            }

            AbilityReactions();
        }
        
    }

    private EnemyController NearbyEnemy()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(this.transform.position, attackRadius);
        float minDistance = 0;
        EnemyController nearestEnemy = null;

        foreach (Collider collider in enemyColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                if (!_nearbyEnemies.Contains(collider.GetComponent<EnemyController>()))
                {
                    float distance = Vector3.Distance(this.transform.position, collider.transform.position);

                    if (minDistance == 0 || distance < minDistance)
                    {
                        nearestEnemy = collider.GetComponent<EnemyController>();
                    }
                }
            }
        }

        return nearestEnemy;
    }

    private void SpawnProjectile()
    {
        ProjectileController projectile = Instantiate(projectilePrefab.GetComponent<ProjectileController>(), projectileSpawn.position, voidMonsterEye.rotation);
        projectile.InitialiseProjectile();
        projectile.ProjectileTarget = _monsterTarget;
    }

    private void AbilityReactions()
    {
        if(_missileCooldown <= 0)
        {
            if (_playerController.ability1.abilityStatus == Ability.AbilityStatus.Active)
            {
                VoidMissiles();
                _missileCooldown = 8;
            }
        }

        if(_abyssCooldown <= 0)
        {
            if(_playerController.ability2.abilityStatus == Ability.AbilityStatus.Active)
            {
                EmptyAbyss();
                _abyssCooldown = 8;
            }
        }

        if(!_chargeActivated)
        {
            if(_playerController.ability3.abilityStatus == Ability.AbilityStatus.Active)
            {
                _chargeActivated = true;
            }
        }
        else
        {
            if (_playerController.ability3.abilityStatus == Ability.AbilityStatus.OnCooldown)
            {
                _navMeshAgent.speed = chargeSpeed;
                _isCharging = true;
                _chargeActivated = false;
            }
        }
    }

    private void VoidMissiles()
    {
        if(_monsterTarget != null)
        {
            
        }
    }

    private void EmptyAbyss()
    {
        Instantiate(emptyAbyssPrefab, this.transform.position + this.transform.TransformDirection(Vector3.down), Quaternion.identity);
    }

    private void LifeForceGrab()
    {

    }

    private IEnumerator StopCharge()
    {
        yield return new WaitForSeconds(chargeDuration);
        _isCharging = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isCharging)
        {
            if(other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyController>().TakeDamage(chargeDamage);
            }
        }
    }
}
