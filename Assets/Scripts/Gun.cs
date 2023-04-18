using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GunData GunData;
    [SerializeField] Transform muzzle;
    private float timeSinceLastShot;
    [SerializeField] Transform mainCamera;
    private BulletTrailEffect trailEffect;


    private Animator Animator;
    private PlayerInputActions inputActions;

    private void Start()
    {
        
        trailEffect = GetComponent<BulletTrailEffect>();
    }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        Animator = GetComponent<Animator>();
    }

    public void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        Shoot();
        StartReload();
        Debug.DrawRay(muzzle.position, muzzle.forward);
    }

    //Gun Actions
     public void StartReload()
    {
        if (!GunData.reloading && inputActions.Player.Reload.WasPressedThisFrame() && GunData.currentAmmo != GunData.magazineSize)
        {
            
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        Animator.SetBool("IsReloading", true);
        GunData.reloading = true;
        yield return new WaitForSeconds(GunData.reloadTime);
        GunData.currentAmmoCount -= GunData.currentAmmo;
        GunData.currentAmmo = GunData.magazineSize;

        GunData.reloading = false;
        Animator.SetBool("IsReloading", false);
    }

    private bool CanShoot() => !GunData.reloading && timeSinceLastShot > 1f / (GunData.fireRate / 60);

    public void Shoot()
    {

        if (GunData.currentAmmo > 0 && !GunData.semiAutomatic && inputActions.Player.Shoot.IsPressed())
        {
            if (CanShoot())
            {
               // Animator.SetBool("IsShooting", true);
               // ShootingSystem.Play();
                //Vector3 direction = GetDirection();

                if (Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit hitInfo, GunData.maxDistance))
                {
                    
                    trailEffect.CreateBulletTrail(muzzle, hitInfo.point);
                    

                    IDamagable damagable = hitInfo.transform.GetComponent<IDamagable>();
                    damagable?.Damage(GunData.damage);
                }

                
                GunData.currentAmmo--;
                timeSinceLastShot = 0;
                
            }
        }
        else if (GunData.currentAmmo > 0 && GunData.semiAutomatic && inputActions.Player.Shoot.WasPressedThisFrame())
        {
            if (CanShoot())
            {
               

                if (Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit hitInfo, GunData.maxDistance))
                {
                    trailEffect.CreateBulletTrail(muzzle, hitInfo.point);
                    Destroy(trailEffect.gameObject, 5);

                    IDamagable damagable = hitInfo.transform.GetComponent<IDamagable>();
                    damagable?.Damage(GunData.damage);
                }


                GunData.currentAmmo--;
                timeSinceLastShot = 0;

            }
        }
    }


}
