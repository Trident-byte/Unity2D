using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : UIBars
{
    // Start is called before the first frame update
    public static HealthBar healthBar;
    void Start()
    {
        healthBar = this;
    }
}
