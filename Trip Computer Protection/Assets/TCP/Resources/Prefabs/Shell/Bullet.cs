using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 70f;
    int damage;
    protected Transform target;
    Vector3 dir, lastTargetPos;
    [SerializeField] protected GameObject hitEffect;

    public void Init(Transform target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }

    protected virtual void Update()
    {       
        if (target == null)
        {
            transform.LookAt(target);
            dir = lastTargetPos;
            GameObject.Destroy(gameObject, 4f);
        }
        else
        {
            lastTargetPos = target.position - transform.position;
            dir = (target.position - transform.position);
        }
        float distancePerFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distancePerFrame)
        {
            Hit();
            return;
        }
        transform.Translate(dir.normalized * distancePerFrame, Space.World);
        transform.rotation = Quaternion.LookRotation(dir);
    }

    protected virtual void Hit()
    {
        HitEffect();
        GameObject.Destroy(gameObject);

        if(target != null) Damage(target);
    }

    protected virtual void Damage(Transform target)
    {
        EnemyController enemy = target.GetComponent<EnemyController>();
        enemy.TakeDamage(damage);
    }

    protected virtual void HitEffect()
    {
        var effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        GameObject.Destroy(effect, 2f);
    }
}
