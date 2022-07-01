using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public void SpawnProjectile(Transform prefab, Vector3 position, Vector3 rotation, float movementSpeed, int damageAmount)
    {
        ProjectileController projectile = Instantiate(prefab.GetComponent<ProjectileController>(), position, Quaternion.identity);
        projectile.ProjectileMovementSpeed = movementSpeed;
        projectile.ProjectileDamage = damageAmount;
        projectile.InitialiseProjectile(); 
        projectile.transform.localEulerAngles = rotation;
    }
}
