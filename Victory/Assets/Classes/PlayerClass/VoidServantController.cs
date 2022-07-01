using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidServantController : PlayerController
{
    [Header("Void Servant Settings")]

    [SerializeField]
    private Transform voidBoltPrefab;

    [SerializeField]
    private Transform emptyAbyssPrefab;

    [SerializeField]
    private Transform supressiveBeamPrefab;

    [SerializeField]
    private float lifeGrabRadius;

    [SerializeField]
    private int lifeGrabDamage;

    [SerializeField]
    private int chaoticBloomDamage;

    [SerializeField]
    private int chaoticBloomRadius;

    [SerializeField]
    private Transform temporalBlinkPrefab;

    [SerializeField]
    private Transform voidMonsterPrefab;

    private GameObject _supressiveBeam;

    private GameObject _temportalBlinkObj;

    public void EndSupressiveBeam()
    {
        if(_supressiveBeam != null)
        {
            disablePlayerMovement = false;
            Destroy(_supressiveBeam);
        }
    }

    public void ActivateTemportalBlink()
    {
        if(_temportalBlinkObj != null)
        {
            TeleportPlayer(_temportalBlinkObj.transform.position);
            Destroy(_temportalBlinkObj);
        }
    }

    public void VoidBolt()
    {
        Instantiate(voidBoltPrefab, this.transform.position + this.transform.TransformDirection(Vector3.forward) * 3, this.transform.rotation);
        GameManager.audioManager.PlayLocalSound("VoidBolt_SFX", this.transform.position);
    }

    public void EmptyAbyss()
    {
        Instantiate(emptyAbyssPrefab, this.transform.position + this.transform.TransformDirection(Vector3.down), this.transform.rotation);
    }

    public void SuppresiveBeam()
    {
        _supressiveBeam = Instantiate(supressiveBeamPrefab.gameObject, this.transform.position, this.transform.rotation, this.transform);
        disablePlayerMovement = true;
    }

    public void LifeForceGrab()
    {
        EnemyController nearestEnemy = FindNearestEnemy();

        if (nearestEnemy == null)
        {
            //ability4.RefreshAbility();
            Debug.Log("Enemy was null");
        }
        else
        {
            nearestEnemy.TakeDamage(lifeGrabDamage);
            Heal(lifeGrabDamage);
        }

        Debug.Log("Life Force Grab Activated");
    }

    public void ChaoticBloom()
    {
        ExplosionDamage(this.transform.position);
        GameManager.spawnManager.SpawnExplosionEffect(this.transform.position, 5);
    }

    private void ExplosionDamage(Vector3 position)
    {
        Collider[] enemyColliders = Physics.OverlapSphere(position, chaoticBloomRadius);

        Vector3 enemyPosition = position;
        bool enemyDefeated = false;

        foreach (Collider collider in enemyColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                EnemyController enemy = collider.GetComponent<EnemyController>();

                if ((enemy.CharacterHealth - chaoticBloomDamage) <= 0)
                {
                    enemyDefeated = true;
                    enemyPosition = enemy.transform.position;
                }

                enemy.TakeDamage(chaoticBloomDamage);
            }
        }

        if(enemyDefeated)
        {
            Collider[] enemiesNearby = Physics.OverlapSphere(enemyPosition, chaoticBloomRadius);

            foreach (Collider collider in enemiesNearby)
            {
                if (collider.CompareTag("Enemy"))
                {
                    EnemyController enemy = collider.GetComponent<EnemyController>();
                    enemy.TakeDamage(chaoticBloomDamage);
                    GameManager.spawnManager.SpawnExplosionEffect(collider.transform.position, 5);
                }
            }
        }
    }

    public void TemporalBlink()
    {
        _temportalBlinkObj = Instantiate(temporalBlinkPrefab.gameObject, this.transform.position + this.transform.TransformDirection(Vector3.forward) * 3, this.transform.rotation);
        Destroy(_temportalBlinkObj, specialAbility2.activeTime);
    }

    public void VoidMonster()
    {
        GameObject obj = Instantiate(voidMonsterPrefab.gameObject, this.transform.position + new Vector3(0, -0.5f, 0) + this.transform.TransformDirection(Vector3.forward), Quaternion.identity);
        Destroy(obj, specialAbility3.activeTime);
        Debug.Log("SPECIAL ABILTIY 3 USED");
    }

    private EnemyController FindNearestEnemy()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(this.transform.position, lifeGrabRadius);
        float minDistance = 0;
        EnemyController nearestEnemy = null;

        foreach (Collider collider in enemyColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(this.transform.position, collider.transform.position);

                if (minDistance == 0 || distance < minDistance)
                {
                    nearestEnemy = collider.GetComponent<EnemyController>();
                }
            }
        }

        return nearestEnemy;
    }
}
