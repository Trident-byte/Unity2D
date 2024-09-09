using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected GameObject explosivePrefab;
    [SerializeField] protected double fireRate;
    [SerializeField] protected Transform firingPoint;
    [SerializeField] protected AudioSource firingAudio;

    public void Shoot(Boolean hasExplosive)
    {
        if (hasExplosive)
        {
            Instantiate(explosivePrefab, firingPoint.position, firingPoint.rotation);
        }
        else
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        }
        firingAudio.Play();

    }

    public double getFireRate()
    {
        return fireRate;
    }
}
