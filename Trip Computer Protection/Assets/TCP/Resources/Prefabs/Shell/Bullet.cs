using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 70f;
    Transform target;
    Vector3 dir;
    [SerializeField] GameObject hitEffect;

    public void Init(Transform target)
    {
        this.target = target;
        
    }

    private void Update()
    {
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

    void Hit()
    {
        HitEffect();
        GameObject.Destroy(gameObject);

        //Not realiset damage!
        GameObject.Destroy(target.gameObject);
    }

    void HitEffect()
    {
        var effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        GameObject.Destroy(effect, 2f);
    }
}
