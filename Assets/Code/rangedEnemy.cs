using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedEnemy : enemy
{
    public float distanceToShoot = 10f;
    public float distanceToStop = 5f;

    [SerializeField] private float fireRate;
    private float timeToFire;
    public Transform firingPoint;
    public GameObject bulletPrefab;

    private void Update()
    {
        //Get the target
        if (!target)
        {
            GetTarget();
        }
        //Rotate to target
        else
        {
            RotateTowardsTarget();
        }

        if (target != null && Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            Shoot();
        }

    }

    private void FixedUpdate()
    {
        if (target != null && Vector2.Distance(target.position, transform.position) >= distanceToStop)
        {
            //Move fowards
            rb.velocity = transform.up * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Shoot()
    {
        if (timeToFire <= 0f)
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }
}
