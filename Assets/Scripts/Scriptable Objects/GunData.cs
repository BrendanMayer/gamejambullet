using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{

    [Header("Info")]
     public new string name;

    [Header("Shooting")]
     public float damage;
     public float maxDistance;
    public bool semiAutomatic;

    [Header("Reloading")]
     public int currentAmmo;
     public int magazineSize;
     public float fireRate;
     public float reloadTime;
     public bool reloading;
    public int maxAmmoCount;
    public int currentAmmoCount;

    //[Header("BulletSpread Settings")]
   // public bool addBulletSpread = true;
   // public float spread;


    
}
