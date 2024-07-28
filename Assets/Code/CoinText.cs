using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    public static CoinText coinText;
    public TextMeshProUGUI moneyText;
    void Start()
    {
        coinText = this;
    }

    public void updateCounter(float money)
    {
        moneyText.text = "Coin: " + money;
    }
}
