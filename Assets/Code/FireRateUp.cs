using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        player.curPlayer.increaseFireRate(0.9);
        Destroy(gameObject);
    }
}
