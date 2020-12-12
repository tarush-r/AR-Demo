using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed = 0.1f;
    private Transform target;
    private float health = 2f;
    private int wavepointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[0];
    }

    void GetNextWaypoint()
    {
        
        wavepointIndex++;
        
        if(wavepointIndex>=Waypoints.points.Length)
        {
            Destroy(gameObject);
        }
        target = Waypoints.points[wavepointIndex];

    }
    // Update is called once per frame
    void Update()
    {
        
        // Time.timeScale=0.03f;
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized*speed*Time.deltaTime);

        if(Vector3.Distance(transform.position, target.position)<=0.01)
        {
            GetNextWaypoint();
        }

    }

    void OnCollisionEnter(Collision col)
    {
        print("COLLISION");
        if(col.transform.tag=="Bullet")
        {
            health-=1;
            if(health<=0)
            {
                Destroy(gameObject);
            }
        }
        Destroy(col.gameObject);
    }
    

    
}
