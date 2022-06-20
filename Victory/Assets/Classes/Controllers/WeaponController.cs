using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private int weaponDamageAmount;

    private bool _isWeaponActive;

    public bool IsWeaponActive
    {
        set { _isWeaponActive = value; }
        get { return _isWeaponActive; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isWeaponActive)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().TakeDamage(weaponDamageAmount);
            }
        }
    }
}
