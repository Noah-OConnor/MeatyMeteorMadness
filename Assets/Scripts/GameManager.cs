using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private int maxLives;
    private int lives;
    private int meat;
    private float gameTime;
    private int score;
    [SerializeField] private int timeScoreBonus;
    private float scoreTimer;

    private PlayerController playerController;
    private InGameUiController inGameUiController;
    private MenuUiController menuUiController;

    [SerializeField] private float initialMeteorSpeed;
    [SerializeField] private float meteorSpeed;
    [SerializeField] private float meteorSpeedMax;
    [SerializeField] private float initialMeteorSpawnInterval;
    [SerializeField] private float meteorSpawnInterval;
    [SerializeField] private float meteorSpawnIntervalMin;
    [SerializeField] private float difficultyMultiplier;
    [SerializeField] private float meatSpawnRateMultiplier;

    [SerializeField] private float horizontalClamp;
    private bool invincible;

    public List<GameObject> objectsToDelete;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        playerController = FindObjectOfType<PlayerController>();
        menuUiController = FindObjectOfType<MenuUiController>();
        inGameUiController = FindObjectOfType<InGameUiController>();

        menuUiController.SetUp();
        inGameUiController.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        scoreTimer += Time.deltaTime;

        if (scoreTimer > 5)
        {
            score += timeScoreBonus * (1 + ((int) gameTime / 60));
            scoreTimer = 0;

            meteorSpeed *= (1 + difficultyMultiplier);
            if (meteorSpeed > meteorSpeedMax)
            {
                meteorSpeed = meteorSpeedMax;
            }

            meteorSpawnInterval *= (1 - difficultyMultiplier);
            if (meteorSpawnInterval < meteorSpawnIntervalMin)
            {
                meteorSpawnInterval = meteorSpawnIntervalMin;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }

    public void EatMeat()
    {
        meat++;
        score += 100;
        if (meat >= 20)
        {
            meat = 0;
            lives++;
        }
    }
    public void DamagePlayer()
    {
        if (!invincible)
        {
            lives--;
            playerController.TakeDamage();
            invincible = true;

            if (lives <= 0)
            {
                LoseGame();
            }
        }
    }

    public void LoseGame()
    {
        // Lose Game
        inGameUiController.gameObject.SetActive(false);
        menuUiController.EnableMainScreen();
        menuUiController.Lose();
        Time.timeScale = 0f;

        foreach (GameObject obj in objectsToDelete)
        {
            Destroy(obj);
        }
    }

    public void ResetGame()
    {
        menuUiController.DisableMainScreen();
        inGameUiController.gameObject.SetActive(true);
        meteorSpeed = initialMeteorSpeed;
        meteorSpawnInterval = initialMeteorSpawnInterval;
        lives = maxLives;
        score = 0;
        gameTime = 0;
        scoreTimer = 0;
        meat = 0;
        Time.timeScale = 1;
        playerController.ResetPosition();
    }

    public float GetGameTime()
    {
        return gameTime;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetLives()
    {
        return lives;
    }

    public int GetMeat()
    {
        return meat;
    }

    public float GetMeteorSpeed()
    {
        return meteorSpeed;
    }

    public float GetMeteorSpawnInterval()
    {
        return meteorSpawnInterval;
    }

    public float GetMeatSpawnRateMultiplier()
    {
        return meatSpawnRateMultiplier;
    }

    public float GetHorizontalClamp()
    {
        return horizontalClamp;
    }

    public void SetInvincible(bool isInvincible)
    {
        invincible = isInvincible;
    }

    public bool GetInvincible()
    {
        return invincible;
    }
}