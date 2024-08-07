using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class RoundUI : MonoBehaviour
{
    public static RoundUI roundUI;
    private int round;
    private int enemies;
    private int enemiesLeft;
    [SerializeField] private TextMeshProUGUI curRound;
    [SerializeField] private TextMeshProUGUI enemiesUI;
    [SerializeField] private GameObject shop;
    void Start()
    {
        roundUI = this;
        LevelManager.manager.newRound();
    }

    public void changeRound(int enemiesForRound)
    {
        round += 1;
        curRound.text = "Round: " + round;
        enemiesLeft = enemiesForRound;
        enemies = enemiesForRound;
        enemiesUI.text = "Enemies Left: " + enemiesLeft + "/" + enemies;
    }

    public void updateEnemies()
    {
        enemiesLeft -= 1;
        if (enemiesLeft == 0)
        {
            shop.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            enemiesUI.text = "Enemies Left: " + enemiesLeft + "/" + enemies;
        }
    }
}
