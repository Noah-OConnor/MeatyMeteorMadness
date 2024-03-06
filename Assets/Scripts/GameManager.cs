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

        lives = maxLives;
        playerController = FindObjectOfType<PlayerController>();
        menuUiController = FindObjectOfType<MenuUiController>();
        meteorSpeed = initialMeteorSpeed;
        meteorSpawnInterval = initialMeteorSpawnInterval;
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
    }

    public void ResetGame()
    {
        menuUiController.gameObject.SetActive(false);
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
