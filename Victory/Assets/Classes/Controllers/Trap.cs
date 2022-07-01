using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private string collisionTag;

    [SerializeField]
    private float trapDuration;

    [SerializeField]
    private int trapInitialDamage;

    [SerializeField]
    private int trapDurationDamage;

    [SerializeField]
    private float trapDamageApplierTime;
    
    [SerializeField]
    private bool disableMovement;

    [SerializeField]
    private float slowDownSpeed;

    private float _trapTimer;

    private float _damageTimer;

    private List<EnemyController> _trappedEnemies = new List<EnemyController>();

    private void Start()
    {
        _trapTimer = trapDuration;
        _damageTimer = trapDamageApplierTime;
    }

    private void Update()
    {
        if(_trapTimer <= 0)
        {
            foreach(EnemyController enemy in _trappedEnemies)
            {
                enemy.EnemyState = EnemyController.AIState.Idle;

                if(slowDownSpeed != 0)
                {
                    enemy.SetMovementSpeed(0); //Resets the enemy's speed with the given value of zero
                }
            }

            _trappedEnemies.Clear();
            this.gameObject.SetActive(false);
        }
        else
        {
            _trapTimer -= Time.deltaTime * 1;

            if (_damageTimer > 0)
            {
                _damageTimer -= Time.deltaTime * 1;
            }
            else
            {
                foreach (EnemyController enemy in _trappedEnemies)
                {
                    enemy.TakeDamage(trapDurationDamage);
                }

                _damageTimer = trapDamageApplierTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_trapTimer > 0)
        {
            if (other.CompareTag(collisionTag))
            {
                if (collisionTag == "Enemy")
                {
                    EnemyController enemy = other.GetComponent<EnemyController>();
                    if (!_trappedEnemies.Contains(enemy))
                    {
                        enemy.TakeDamage(trapInitialDamage);

                        if (disableMovement)
                        {
                            enemy.EnemyState = EnemyController.AIState.Disabled;
                        }

                        if (slowDownSpeed != 0)
                        {
                            enemy.SetMovementSpeed(slowDownSpeed);
                        }

                        _trappedEnemies.Add(enemy);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(collisionTag))
        {
            if (collisionTag == "Enemy")
            {
                EnemyController enemy = other.GetComponent<EnemyController>();

                if(_trappedEnemies.Contains(enemy))
                {
                    if (slowDownSpeed != 0)
                    {
                        enemy.SetMovementSpeed(0); //Resets the enemy's speed with the given value of zero
                    }

                    _trappedEnemies.Remove(enemy);
                }
            }
        }
    }
}
