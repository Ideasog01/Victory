using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform weaponDropPrefab;

    [SerializeField]
    private Transform voidExplosionPrefab;

    public void SpawnWeaponDrop(Weapon weapon, Vector3 position)
    {
        WeaponInteractive weaponInteractive = Instantiate(weaponDropPrefab.GetComponent<WeaponInteractive>(), position, Quaternion.identity);
        weaponInteractive.SetWeapon(weapon);
    }

    public void SpawnExplosionEffect(Vector3 position, float duration)
    {
        GameObject explosion = Instantiate(voidExplosionPrefab.gameObject, position, Quaternion.Euler(90, 0, 0));
        Destroy(explosion, duration);
    }
}
