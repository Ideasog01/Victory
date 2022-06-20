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

    private float _trapTimer;

    private EnemyController _trapedEnemy;

    private void Update()
    {
        if(_trapedEnemy != null)
        {
            if (_trapTimer <= 0)
            {
                _trapedEnemy.EnemyState = EnemyController.AIState.Idle;
                this.gameObject.SetActive(false);
            }
            else
            {
                _trapTimer -= Time.deltaTime * 1;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_trapedEnemy == null)
        {
            if (other.CompareTag(collisionTag))
            {
                if (collisionTag == "Enemy")
                {
                    EnemyController enemy = other.GetComponent<EnemyController>();
                    enemy.TakeDamage(trapInitialDamage);
                    enemy.EnemyState = EnemyController.AIState.Disabled;
                    _trapTimer = trapDuration;
                    _trapedEnemy = enemy;
                }
            }
        }
    }
}
