using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using Unity.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager manager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject deathScreen;
    [SerializeField] GameObject[] obstacles;

    public SaveData data;
    public int curScore;

    private void Awake()
    {
        manager = this;
        SaveSystem.Initalize();
        GenerateObjects(50);
        data = new SaveData(0);
    }

    public void GameOver()
    {
        deathScreen.SetActive(true);
        scoreText.text = "Score: " + curScore.ToString();

        string loadedData = SaveSystem.Load("save");
        if (loadedData != null)
        {
            data = JsonUtility.FromJson<SaveData>(loadedData);
        }

        if (data.highScore < curScore)
        {
            data.highScore = curScore;
        }

        highScoreText.text = "HighScore: " + data.highScore.ToString();
        string saveData = JsonUtility.ToJson(data);
        SaveSystem.Save("save", saveData);
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncreaaseScore(int amount)
    {
        curScore += amount;
    }

    private void GenerateObjects(int numOfObjects)
    {
        for (int i = 0; i < numOfObjects; i++)
        {
            int rand_x = Random.Range(-45, 46) + 5;
            int rand_y = Random.Range(-45, 46) + 5;
            int randObject = Random.Range(0, obstacles.Length);
            UnityEngine.Vector3 position = new UnityEngine.Vector3(rand_x, rand_y, 0);
            Instantiate(obstacles[randObject], position, Quaternion.identity);
        }
    }
}

public class SaveData
{
    public int highScore;
    public SaveData(int hs)
    {
        highScore = hs;
    }
}
