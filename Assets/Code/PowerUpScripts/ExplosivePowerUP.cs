using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivePowerUP : MonoBehaviour
{
    [SerializeField] private GameObject explosivePrefab;
    // Start is called before the first frame update
    void Start()
    {
        player.curPlayer.switchBulletType(explosivePrefab);   
    }
}
