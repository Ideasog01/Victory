using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidBoltController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float checkRadius;
    
    [SerializeField]
    private int projectileDamage;

    [SerializeField]
    private float projectileDuration;

    [SerializeField]
    private EnemyController _projectileTarget;

    private float _durationTimer;

    private List<EnemyController> _foundEnemies = new List<EnemyController>();

    private void Start()
    {
        FindNearestEnemy();
        _durationTimer = projectileDuration;
    }

    private void Update()
    {
        if(_projectileTarget != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _projectileTarget.transform.position, Time.deltaTime * movementSpeed);
        }
        else
        {
            this.transform.position += this.transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }

        if(_durationTimer > 0)
        {
            _durationTimer -= Time.deltaTime * 1;
        }
        else
        {
            VoidExplosion();
            Destroy(this.gameObject);
        }
    }

    private void FindNearestEnemy()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(this.transform.position, checkRadius);
        float minDistance = 0;
        EnemyController nearestEnemy = null;

        foreach(Collider collider in enemyColliders)
        {
            if(collider.CompareTag("Enemy"))
            {
                if(!_foundEnemies.Contains(collider.GetComponent<EnemyController>()))
                {
                    float distance = Vector3.Distance(this.transform.position, collider.transform.position);

                    if(minDistance == 0 || distance < minDistance)
                    {
                        nearestEnemy = collider.GetComponent<EnemyController>();
                    }
                }
            }
        }

        if(nearestEnemy != null)
        {
            _projectileTarget = nearestEnemy;
            _foundEnemies.Add(nearestEnemy);
        }
        else
        {
            if(_projectileTarget != null)
            {
                VoidExplosion();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(_projectileTarget != null && other.gameObject == _projectileTarget.gameObject)
            {
                _projectileTarget.TakeDamage(projectileDamage);
                FindNearestEnemy();
            }
            else
            {
                other.GetComponent<EnemyController>().TakeDamage(projectileDamage);
                FindNearestEnemy();
            }
        }
    }

    private void VoidExplosion()
    {
        if(_projectileTarget != null)
        {
            Collider[] enemyColliders = Physics.OverlapSphere(this.transform.position, checkRadius);
            foreach (Collider collider in enemyColliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    _projectileTarget.TakeDamage(projectileDamage);
                    GameManager.spawnManager.SpawnExplosionEffect(this.transform.position, 3);
                    //Spawn VFX
                    //Play SFX
                }
            }
        }
    }
}
