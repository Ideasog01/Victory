using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteractive : Interactable
{
    [SerializeField]
    private Weapon weaponToEquip;

    public void EquipWeapon()
    {
        Player.SetWeapon(weaponToEquip);
        Destroy(this.gameObject);
    }

    public void SetWeapon(Weapon weapon)
    {
        weaponToEquip = weapon;

        this.transform.GetChild(0).GetComponent<MeshRenderer>().material = weaponToEquip.weaponMaterial;
        this.transform.GetChild(0).GetComponent<MeshFilter>().mesh = weaponToEquip.weaponMesh;

        this.transform.GetChild(0).localScale = weaponToEquip.weaponScale;
    }
}
