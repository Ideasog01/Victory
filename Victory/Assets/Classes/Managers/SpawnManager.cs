using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform weaponDropPrefab;

    public void SpawnWeaponDrop(Weapon weapon, Vector3 position)
    {
        WeaponInteractive weaponInteractive = Instantiate(weaponDropPrefab.GetComponent<WeaponInteractive>(), position, Quaternion.identity);
        weaponInteractive.SetWeapon(weapon);
    }
}
