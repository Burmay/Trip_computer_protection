using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    float speed;
    [SerializeField] float startSpeed = 10f;
    [SerializeField] float offset = 0.2f;
    [SerializeField] float health;
    [SerializeField] float startHealth = 100;

    Transform target;
    int wavePointIndex = 0;
    [SerializeField] Image healthBar;
    

    private void Start()
    {
        target = WayPointController.points[0];
        health = startHealth;
        speed = startSpeed;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= offset) { GetNextWayPoint(); }

        if(speed != startSpeed) speed = startSpeed;
    }

    void GetNextWayPoint()
    {
        if(wavePointIndex >= WayPointController.points.Length - 1) { GameObject.Destroy(gameObject); return; }
        else
        {
            wavePointIndex++;
            target = WayPointController.points[wavePointIndex];
        }
    }

    public void TakeDamage(float count)
    {
        health -= count;
        healthBar.fillAmount = health / startHealth;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Slow(float count)
    {
        speed = startSpeed * (1f - count);
    }

    void Die()
    {
        GameObject.Destroy(gameObject);
    }
}
