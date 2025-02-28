using System.Collections.Generic;
using UnityEngine;

// DESIGN PATTERN: Singleton, Observer
// We want to ensure that only one game manager exists.
// If another existed, it could make different choices
// or provide different information than what is intended,
// ruining the experience of the game.
// Implements the Observer pattern using C# events to update
// the HUD UI and disable meteors and meats when the player loses

public class _GameManager : MonoBehaviour
{
    public static _GameManager instance;
    [SerializeField] private int maxLives;
    private int lives;
    private int totalMeat;
    private int currentMeat;
    private int previousGameTime;
    private float gameTime;
    private int score;
    [SerializeField] private int timeScoreBonus;
    private float scoreTimer;

    private _PlayerController playerController;
    private HUDController inGameUiController;
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

    public bool munch = false;
    public bool hurt = false;

    public bool lose = true;

    // Event that observers can subscribe to
    public delegate void IntUpdated(int newScore);
    public static event IntUpdated OnScoreUpdated;
    public static event IntUpdated OnLivesUpdated;
    public static event IntUpdated OnTimeUpdated;
    public static event IntUpdated OnMeatCountUpdated;

    public delegate void GameOver();
    public static event GameOver OnGameOver;

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

        playerController = FindFirstObjectByType<_PlayerController>();
        menuUiController = FindFirstObjectByType<MenuUiController>();
        inGameUiController = FindFirstObjectByType<HUDController>();

        menuUiController.SetUp();
        playerController.gameObject.SetActive(false);
        inGameUiController.gameObject.SetActive(false);
        lose = true;
    }

    private void Update()
    {
        if (lose) return;
        gameTime += Time.deltaTime;
        if ((int)gameTime != previousGameTime)
        {
            SetTime(gameTime);
        }
        scoreTimer += Time.deltaTime;

        if (scoreTimer > 5)
        {
            SetScore(score + (timeScoreBonus * (1 + ((int)gameTime / 60))));
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
    }

    private void SetScore(int amount)
    {
        score = amount;
        OnScoreUpdated?.Invoke(score);
    }

    private void SetLives(int amount)
    {
        lives = amount;
        OnLivesUpdated?.Invoke(lives);
    }

    private void SetTime(float amount)
    {
        previousGameTime = (int)gameTime;
        OnTimeUpdated?.Invoke(previousGameTime);
    }

    private void SetMeatCount(int amount)
    {
        currentMeat = amount;
        OnMeatCountUpdated?.Invoke(currentMeat);
    }

    public void EatMeat()
    {
        munch = true;
        AudioManager.instance.Play(7);
        totalMeat++;
        SetMeatCount(currentMeat + 1);
        SetScore(score + 100);
        if (currentMeat >= 20)
        {
            SetMeatCount(0);
            SetLives(lives + 1);
        }
    }

    public void DamagePlayer()
    {
        if (!invincible)
        {
            AudioManager.instance.Play(3);
            AudioManager.instance.Play(2);
            SetLives(lives - 1);
            playerController.TakeDamage();
            invincible = true;

            if (lives <= 0)
            {
                LoseGame();
            }
            else
            {
                hurt = true;
            }
        }
    }

    public void LoseGame()
    {
        // Lose Game
        lose = true;
        StartCoroutine(AudioManager.instance.Death());
        inGameUiController.gameObject.SetActive(false);
        menuUiController.DisableMainScreen();
        menuUiController.EnableLoseScreen();
        menuUiController.EnableStatsScreen();
        menuUiController.Lose();
        playerController.gameObject.SetActive(false);
        CancelInvoke(nameof(SetTime));
        OnGameOver?.Invoke();
    }

    public void ResetGame()
    {
        lose = false;
        hurt = false;
        munch = false;
        StartCoroutine(AudioManager.instance.StartGame());
        menuUiController.DisableMainScreen();
        menuUiController.DisableLoseScreen();
        menuUiController.DisableStatsScreen();
        inGameUiController.gameObject.SetActive(true);
        meteorSpeed = initialMeteorSpeed;
        meteorSpawnInterval = initialMeteorSpawnInterval;
        SetLives(maxLives);
        SetScore(0);
        gameTime = 0;
        scoreTimer = 0;
        totalMeat = 0;
        SetMeatCount(0);
        playerController.gameObject.SetActive(true);
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

    public int GetTotalMeat()
    {
        return totalMeat;
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