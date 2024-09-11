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
    private Vector3 test;

    public void Shoot(bool hasExplosive, Transform player)
    {
        Debug.Log(transform.position.Equals(test));
        Debug.Log(test);
        if (hasExplosive)
        {
            Instantiate(explosivePrefab, player.transform.position + transform.position, player.transform.rotation * transform.rotation);
        }
        else
        {
            Instantiate(bulletPrefab, player.transform.position + transform.position, player.transform.rotation * transform.rotation);
        }
        firingAudio.Play();

    }

    public double getFireRate()
    {
        return fireRate;
    }

    void Update()
    {
        test = transform.position;
        // Debug.Log(test);
    }
}
