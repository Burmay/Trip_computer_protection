using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 70f;
    int damage;
    Transform target;
    Vector3 dir;
    [SerializeField] protected GameObject hitEffect;

    public void Init(Transform target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }

    protected virtual void Update()
    {
        transform.LookAt(target);

        if (target == null)
        {
            GameObject.Destroy(gameObject, 3f);
            return;
        }
        dir = (target.position - transform.position);
        float distancePerFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distancePerFrame)
        {
            Hit();
            return;
        }
        transform.Translate(dir.normalized * distancePerFrame, Space.World);
    }

    protected virtual void Hit()
    {
        HitEffect();
        GameObject.Destroy(gameObject);

        Damage(target);
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
