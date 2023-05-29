using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretController : TurretController
{
    LineRenderer lineRenderer;
    GameObject activeShotEffect;
    [SerializeField] float slowAmount = 0.7f;
    [SerializeField] bool IfSlow;

    EnemyController targetEnemy;

    protected override void Start()
    {
        base.Start();
        lineRenderer = GetComponent<LineRenderer>();
        damage = 50;
    }

    protected override void Update()
    {
        base.FindNearbyTarget();
        if(target !=  null ) targetEnemy = target.GetComponent<EnemyController>();
        base.Spread();
        Laser();
    }

    void Laser()
    {
        if (!LaserState()) return;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
        ShotEffect();
        TakeDamage();
        if(IfSlow) Slowtarget();
    }

    bool LaserState()
    {
        if (target == null || shortestDistance > range || !base.TargetAcquired())
        {
            if(lineRenderer.enabled) lineRenderer.enabled = false;
            if(activeShotEffect != null) { Destroy(activeShotEffect); }
            return false;
        }
        else
        {
            if(!lineRenderer.enabled) lineRenderer.enabled = true;
            return true;
        }
    }

    protected override void ShotEffect()
    {
        if(activeShotEffect == null) activeShotEffect = Instantiate(shotEffect, target.position, Quaternion.identity);
        else
        {
            Vector3 dir = firePoint.position - target.position;
            activeShotEffect.transform.position = target.position + dir.normalized*0.25f;
            activeShotEffect.transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    void TakeDamage()
    {
        targetEnemy.TakeDamage(damage * Time.deltaTime);
    }

    void Slowtarget()
    {
        targetEnemy.Slow(slowAmount);
    }
}