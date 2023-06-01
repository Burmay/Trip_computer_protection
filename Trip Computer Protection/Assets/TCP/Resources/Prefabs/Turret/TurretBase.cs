using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TurretBase : MonoBehaviour
{
    [Header("Attributes")]

    [SerializeField] protected float range = 4f;
    [SerializeField] protected float angularSpeed = 5f;
    [SerializeField] protected float fireRate = 1.0f;
    [SerializeField] protected float fireCountdown = 0f;
    [SerializeField] protected int clipSize = 1;
    [SerializeField] protected int damage;
    [SerializeField] protected int costReload;

    [Header("Ammo")]
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected int sleeveSize;

    [Header("Unity Setup")]

    [SerializeField] Transform partToRotate;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected GameObject shotEffect;
    [SerializeField] Image ammoBar;
    protected float ammo;
    protected string BULLET_PATH;
    protected Bullet bulletPrefab;
    protected Transform target;
    protected WaweInteractor waweInteractor;
    protected BuildInteractor buildInteractor;
    List<GameObject> enemies;
    List<Bullet> bullets;
    protected float shortestDistance;
    InventoryInteractor inventoryInteractor;
    public bool off { get; protected set; }


    protected virtual void Start()
    {
        LoadResources();
        ammo = maxAmmo;
        off = false;
    }

    protected virtual void LoadResources()
    {
        waweInteractor = Game.GetInteractor<WaweInteractor>();
        buildInteractor = Game.GetInteractor<BuildInteractor>();
        bulletPrefab = Resources.Load<Bullet>(BULLET_PATH);
        inventoryInteractor = Game.GetInteractor<InventoryInteractor>();
        bullets = new List<Bullet>();
    }

    protected virtual void Update()
    {
        if (off) return;
        FindNearbyTarget();
        Spread();
        if (CheckOpportunityShot())
        {
            Shoot();
            ShotEffect();
        }
    }

    protected virtual bool CheckOpportunityShot()
    {
        //Check Reload
        if(fireCountdown <= 0)
        {
            // Check target & distance
            if (target != null && shortestDistance <= range)
            {
                // Check the turn on target 
                if (!TargetAcquired()) return false;
                fireCountdown = 1f / fireRate;
                // Check ammo & spend ammo
                if (ammo > 0)
                {
                    SpendAmmo(); return true;
                }
                else return false;
            }
            return false;
        }
        else
        {
            fireCountdown -= Time.deltaTime;
            return false;
        }
    }

    protected virtual void SpendAmmo()
    {
        ammo -= 1;
        DrawAmmo();
    }

    protected virtual void Shoot()
    {
        bullets.Add(Instantiate(bulletPrefab, firePoint.position, Quaternion.identity));
        bullets[bullets.Count - 1].Init(target, damage);
    }

    protected virtual void Spread()
    {
        if (target == null) return;
        Vector3 dir = (target.position - transform.position).normalized;
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, Quaternion.LookRotation(dir), Time.deltaTime * angularSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    protected virtual bool TargetAcquired()
    {
        if (target == null) return false;
        Vector3 dir =(target.position - transform.position).normalized;
        //Debug.Log(Quaternion.LookRotation(dir).eulerAngles.y - partToRotate.rotation.eulerAngles.y);
        if (Math.Abs(Quaternion.LookRotation(dir).eulerAngles.y - partToRotate.rotation.eulerAngles.y) > 5) return false;
        return true;
    }

    protected virtual void ShotEffect()
    {

    }

    protected void DrawAmmo()
    {
        ammoBar.fillAmount = (float)ammo / (float)maxAmmo;
        if (ammo == 0) off = true;
    }

    protected virtual void FindNearbyTarget()
    {
        enemies = waweInteractor.Enemies;
        if (enemies == null) return;

        shortestDistance = float.MaxValue;
        GameObject nearestEnemy = null;
        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void Reload()
    {
        if (inventoryInteractor.SpendSupplies(costReload))
        {
            ammo = maxAmmo;
            off = false;
        }
    }

    public void WaweReload()
    {
        ammo = maxAmmo;
        DrawAmmo();
        if (off) off = !off;
    }
}

public enum TurretType
{
    machineGun
}
