using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalBlink : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private int startDamage;

    [SerializeField]
    private int damageIncrease;

    private int _currentDamage;

    private void Start()
    {
        _currentDamage = startDamage;
    }

    private void Update()
    {
        this.transform.position += this.transform.TransformDirection(Vector3.forward) * Time.deltaTime * projectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.TakeDamage(_currentDamage);
            _currentDamage += damageIncrease;
        }
    }


}
