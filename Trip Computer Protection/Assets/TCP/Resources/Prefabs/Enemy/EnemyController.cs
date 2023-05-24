using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float offset = 0.2f;

    Transform target;
    int wavePointIndex = 0;

    private void Start()
    {
        target = WayPointController.points[0];
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
}
