using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static List<ProjectileController> arrowProjectileList = new List<ProjectileController>();
    public static List<ProjectileController> arrowExplosiveProjectileList = new List<ProjectileController>();
    public static List<ProjectileController> arrowPoisonProjectileList = new List<ProjectileController>();
    public static List<ProjectileController> daggerProjectileList = new List<ProjectileController>();

    [SerializeField]
    private Transform arrowProjectilePrefab;

    [SerializeField]
    private Transform arrowExplosivePrefab;

    [SerializeField]
    private Transform arrowPoisonPrefab;

    [SerializeField]
    private Transform daggerProjectilePrefab;

    public void SpawnArrowProjectile(Vector3 startPosition, Vector3 startRotation, Vector3 projectileMovementSpeed, int projectileDamage)
    {
        bool projectileFound = false;

        foreach(ProjectileController projectile in arrowProjectileList)
        {
            if(!projectile.isActiveAndEnabled)
            {
                projectile.gameObject.SetActive(true);
                projectile.transform.position = startPosition;
                projectile.transform.localEulerAngles = startRotation;
                projectile.ProjectileMovementSpeed = projectileMovementSpeed;
                projectile.ProjectileDamage = projectileDamage;
                projectile.InitialiseProjectile();
                projectileFound = true;
                break;
            }
        }

        if(!projectileFound)
        {
            ProjectileController projectile = Instantiate(arrowProjectilePrefab.GetComponent<ProjectileController>(), startPosition, Quaternion.identity);
            projectile.transform.localEulerAngles = startRotation;
            projectile.ProjectileMovementSpeed = projectileMovementSpeed;
            projectile.ProjectileDamage = projectileDamage;
            projectile.InitialiseProjectile();
            arrowProjectileList.Add(projectile);
        }
    }

    public void SpawnExplosiveArrowProjectile(Vector3 startPosition, Vector3 startRotation, Vector3 projectileMovementSpeed, int projectileDamage)
    {
        bool projectileFound = false;

        foreach (ProjectileController projectile in arrowExplosiveProjectileList)
        {
            if (!projectile.isActiveAndEnabled)
            {
                projectile.gameObject.SetActive(true);
                projectile.transform.position = startPosition;
                projectile.transform.localEulerAngles = startRotation;
                projectile.ProjectileMovementSpeed = projectileMovementSpeed;
                projectile.ProjectileDamage = projectileDamage;
                projectile.InitialiseProjectile();
                projectileFound = true;
                break;
            }
        }

        if (!projectileFound)
        {
            ProjectileController projectile = Instantiate(arrowExplosivePrefab.GetComponent<ProjectileController>(), startPosition, Quaternion.identity);
            projectile.transform.localEulerAngles = startRotation;
            projectile.ProjectileMovementSpeed = projectileMovementSpeed;
            projectile.ProjectileDamage = projectileDamage;
            projectile.InitialiseProjectile();
            arrowExplosiveProjectileList.Add(projectile);
        }
    }

    public void SpawnPoisonArrowProjectile(Vector3 startPosition, Vector3 startRotation, Vector3 projectileMovementSpeed, int projectileDamage)
    {
        bool projectileFound = false;

        foreach (ProjectileController projectile in arrowPoisonProjectileList)
        {
            if (!projectile.isActiveAndEnabled)
            {
                projectile.gameObject.SetActive(true);
                projectile.transform.position = startPosition;
                projectile.transform.localEulerAngles = startRotation;
                projectile.ProjectileMovementSpeed = projectileMovementSpeed;
                projectile.ProjectileDamage = projectileDamage;
                projectile.InitialiseProjectile();
                projectileFound = true;
                break;
            }
        }

        if (!projectileFound)
        {
            ProjectileController projectile = Instantiate(arrowPoisonPrefab.GetComponent<ProjectileController>(), startPosition, Quaternion.identity);
            projectile.transform.localEulerAngles = startRotation;
            projectile.ProjectileMovementSpeed = projectileMovementSpeed;
            projectile.ProjectileDamage = projectileDamage;
            projectile.InitialiseProjectile();
            arrowPoisonProjectileList.Add(projectile);
        }
    }

    public void SpawnDaggerProjectile(Vector3 startPosition, Vector3 startRotation)
    {
        bool projectileFound = false;

        foreach (ProjectileController projectile in daggerProjectileList)
        {
            if (!projectile.isActiveAndEnabled)
            {
                projectile.gameObject.SetActive(true);
                projectile.transform.position = startPosition;
                projectile.transform.localEulerAngles = startRotation;
                projectile.InitialiseProjectile();
                projectileFound = true;
                break;
            }
        }

        if (!projectileFound)
        {
            ProjectileController projectile = Instantiate(daggerProjectilePrefab.GetComponent<ProjectileController>(), startPosition, Quaternion.identity);
            projectile.transform.localEulerAngles = startRotation;
            projectile.InitialiseProjectile();
            daggerProjectileList.Add(projectile);
        }
    }
}
