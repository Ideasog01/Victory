using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupressiveBeamController : MonoBehaviour
{
    [SerializeField]
    private float pushbackForce;

    [SerializeField]
    private int beamDamage;

    [SerializeField]
    private float damageTime;

    private float _damageTimer;

    private List<EnemyController> _enemyList = new List<EnemyController>();

    private void Update()
    {
        if(_damageTimer > 0)
        {
            _damageTimer -= Time.deltaTime * 1;
        }
        else
        {
            foreach (EnemyController enemy in _enemyList)
            {
                enemy.TakeDamage(beamDamage);
            }

            _damageTimer = damageTime;
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if(!_enemyList.Contains(enemy))
            {
                _enemyList.Add(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if(_enemyList.Contains(enemy))
        {
            _enemyList.Remove(enemy);
        }
    }
}
