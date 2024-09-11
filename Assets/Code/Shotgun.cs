using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public void Shoot(bool hasExplosive, Transform player)
    {
        for (int i = 0; i < 5; i++)
        {
            Quaternion rand_angle = Quaternion.Euler(0, 0, Random.Range(-85, 85));
            Instantiate(bulletPrefab, player.transform.position, player.transform.rotation * rand_angle);
        }
    }
}
