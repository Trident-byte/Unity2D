using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBars : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image image;
    public void changeBar(float max, float current)
    {
        image.fillAmount = current / max;
    }
}
