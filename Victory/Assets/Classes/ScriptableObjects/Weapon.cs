using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    [Header("General Settings")]

    public string weaponName;

    public int weaponDamage;

    public Vector3 weaponColliderSize;

    public Vector3 weaponColliderOffset;

    public float weaponAttackCooldown;

    public float maxWeaponCharge;

    public float weaponChargeRate;

    [Header("Display Settings")]

    public Mesh weaponMesh;

    public Material weaponMaterial;

    public Vector3 weaponScale;

    public Vector3 weaponOffset;

    public Sprite weaponIcon;

    [Header("Melee Settings")]

    public bool isMelee;

    [Header("Projectile Settings")]

    public float projectileSpeed;

    public Transform projectilePrefab;



    
}