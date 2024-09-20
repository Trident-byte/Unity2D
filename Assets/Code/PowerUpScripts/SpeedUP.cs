using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUP : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        player.curPlayer.setSpeed(1.02);
        Destroy(gameObject);
    }
}
