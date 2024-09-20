using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public override void Shoot(bool hasExplosive, Transform player)
    {
        Debug.Log("test");
        for (int i = 0; i < 3; i++)
        {
            Quaternion rand_angle = Quaternion.Euler(0, 0, Random.Range(-45, 45));
            Instantiate(bulletPrefab, player.transform.position, player.transform.rotation * rand_angle);
        }
    }
}
