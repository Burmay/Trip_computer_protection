using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : Bullet
{
    [SerializeField] float explosionRadius = 1f;
    bool selfDestruction = false;

    protected override void Update()
    {
        base.Update();
        if(base.target == null)
        {
            if (!selfDestruction)
            {
                Invoke("Exploide", 1f);
                Invoke("HitEffect", 1f);
                GameObject.Destroy(gameObject, 1.05f);
                selfDestruction = true;
            }
        }
    }

    protected override void Hit()
    {
        base.Hit();
        Exploide();
    }

    protected void Exploide()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                base.Damage(collider.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
