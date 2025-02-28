using System.Collections.Generic;
using UnityEngine;

// DESIGN PATTERN: Singleton
// We want to ensure that only one game manager exists.
// If another existed, it could make different choices
// or provide different information than what is intended,
// ruining the experience of the game.

public class _GameManager : MonoBehaviour
{
    public static _GameManager instance;
    [SerializeField] private int maxLives;
    private int lives;
    private int totalMeat;
    private int currentMeat;
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
        scoreTimer += Time.deltaTime;

        if (scoreTimer > 5)
        {
            score += timeScoreBonus * (1 + ((int)gameTime / 60));
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

    public void EatMeat()
    {
        munch = true;
        AudioManager.instance.Play(7);
        totalMeat++;
        currentMeat++;
        score += 100;
        if (currentMeat >= 20)
        {
            currentMeat = 0;
            lives++;
        }
    }

    public void DamagePlayer()
    {
        if (!invincible)
        {
            AudioManager.instance.Play(3);
            AudioManager.instance.Play(2);
            lives--;
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
        //Time.timeScale = 0f;

        foreach (GameObject obj in objectsToDelete)
        {
            obj.SetActive(false);
        }
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
        lives = maxLives;
        score = 0;
        gameTime = 0;
        scoreTimer = 0;
        totalMeat = 0;
        currentMeat = 0;
        //Time.timeScale = 1;
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