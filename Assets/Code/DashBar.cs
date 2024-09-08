using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBar : UIBars
{
    public static DashBar dashbar;
    // Start is called before the first frame update
    void Start()
    {
        dashbar = this;
    }
}
