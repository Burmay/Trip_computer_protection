using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Attributes")]

    [SerializeField] float range = 4f;
    [SerializeField] float angularSpeed = 5f;
    [SerializeField] float fireRate = 1.0f;
    [SerializeField] float fireCountdown = 0f;
    [SerializeField] int clipSize = 1;
    TurretType turretType;

    [Header("Unity Setup")]

    [SerializeField] Transform partToRotate;
    [SerializeField] Transform firePoint;
    [SerializeField] protected string MachineGun_Bullet_Path = "Prefabs/Shell/MachineGun/MachineGunBullet";
    Bullet bulletPrefab;
    Transform target;
    WaweInteractor waweInteractor;
    List<GameObject> enemies;
    List <Bullet> bullets;
    float shortestDistance;



    void Start()
    {
        LoadResources();
        Initialize();
    }

    void LoadResources()
    {
        waweInteractor = Game.GetInteractor<WaweInteractor>();
        bulletPrefab = Resources.Load<Bullet>(MachineGun_Bullet_Path);
    }

    void Initialize()
    {
        bullets = new List<Bullet>();
    }

    private void Update()
    {
        FindNearbyTarget();
        Spread();
        CheckOpportunityShot();
    }

    void CheckOpportunityShot()
    {
        if(fireCountdown <= 0)
        {
            if(target != null && shortestDistance <= range)
            {
                Shoot();
                Debug.Log(fireCountdown);
                fireCountdown = 1f / fireRate;
            }
        }
        else
        {
            fireCountdown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        bullets.Add(Instantiate(bulletPrefab, firePoint.position, Quaternion.identity));
        bullets[bullets.Count - 1].Init(target);
    }

    void Spread()
    {
        if (target == null) return;
        Vector3 dir = (target.position - transform.position).normalized;
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, Quaternion.LookRotation(dir), Time.deltaTime * angularSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void FindNearbyTarget()
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

     void OnDrawGizmosSelected()
     {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
     }
}

public enum TurretType
{
    machineGun
}
