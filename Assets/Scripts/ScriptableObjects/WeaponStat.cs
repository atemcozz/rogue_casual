using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponStat : ItemStat
{
    public enum DamageType{Melee,Ranged,Magic,Throwing}
    public enum WeaponType{Shortsword, Longsword,Bow,Crossbow,Staff, Throwing}
    [SerializeField] Sprite sprite;
     [SerializeField] DamageType damageType;
     [SerializeField] WeaponType weaponType;  
    [Min(0)] [SerializeField] int baseDamage;
    [Min(0)] [SerializeField] float attackTime;
    [Min(0)] [SerializeField] float criticalChance;
    [Min(0)] [SerializeField] float projectileSpeed;
    [Min(0)] [SerializeField] float reloadTime;
    [Min(0)] [SerializeField] float spread;
    [Min(0)] [SerializeField] int baseAmmo;
    [Min(0)] [SerializeField] float attackRadius;
    public int Ammo;
    [SerializeField] bool usesAmmo;
    [SerializeField] GameObject projectile;
    [Min(0)] [SerializeField] float buffChance;
    [SerializeField] bool rotatable;
    #region  GETTERS
    public Sprite Sprite => sprite;
    public DamageType _DamageType => damageType;
    public WeaponType _WeaponType => weaponType;  
    public int BaseDamage => baseDamage;
    public float AttackTime => attackTime;
    public float CriticalChance => criticalChance;
    public float ProjectileSpeed => projectileSpeed;
    public GameObject Projectile => projectile;
    public float BuffChance => buffChance;
    public bool Rotatable => rotatable;
    public float Spread => spread;
    public float ReloadTime => reloadTime;
    public bool UsesAmmo => usesAmmo;
    public float AttackRadius => attackRadius;
    #endregion


}
