using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private float range = 0.00001f;
    public GameObject map;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private float fireRate = 2f;

    private float fireCountdown =0f;

    public Transform partToRotate;
    public string enemyTag = "Enemy";
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map");
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(fireCountdown<=0f)
        {
            Shoot();
            fireCountdown = 1f/fireRate;
        }
        fireCountdown-=Time.deltaTime;

    }

    void Shoot()
    {
        GameObject spawnedBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        spawnedBullet.transform.SetParent(map.transform);
        Bullet bullet = spawnedBullet.GetComponent<Bullet>();

        if(bullet!=null)
        {
            bullet.Seek(target);
        }
        // print("shoot");
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            if (nearestEnemy != null)
            {
                target = nearestEnemy.transform;
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
