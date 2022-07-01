using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidVortex : MonoBehaviour
{
    [SerializeField]
    private float damageDuration;

    [SerializeField]
    private int damageAmount;

    private List<EnemyController> _nearbyEnemies = new List<EnemyController>();

    private float _damageTimer;


    private void Update()
    {
        if (_damageTimer > 0)
        {
            _damageTimer -= Time.deltaTime * 1;
        }
        else
        {
            DamageNearbyEnemies();
            
            _damageTimer = damageDuration;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if(!_nearbyEnemies.Contains(enemy))
            {
                _nearbyEnemies.Add(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (_nearbyEnemies.Contains(enemy))
            {
                _nearbyEnemies.Remove(enemy);
            }
        }
    }

    private void DamageNearbyEnemies()
    {
        foreach(EnemyController enemy in _nearbyEnemies)
        {
            enemy.TakeDamage(damageAmount);

            Vector3 force = enemy.transform.TransformDirection(this.transform.position);
            enemy.ApplyForce(force, 20, damageDuration);
        }
    }
}
