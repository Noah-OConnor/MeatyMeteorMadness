using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUiController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text extraLivesText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text meatCountText;
    private int lastScore;
    private int lastLives;
    private int lastMeatCount;

    [SerializeField] private GameObject hpIcon1;
    [SerializeField] private GameObject hpIcon2;
    [SerializeField] private GameObject hpIcon3;
    [SerializeField] private GameObject meatIcon;

    private void Start()
    {
        lastScore = 0;
        lastLives = 0;
        lastMeatCount = 0;
        UpdateScore();
    }

    private void Update()
    {
        if (GameManager.instance.GetScore() != lastScore)
        {
            lastScore = GameManager.instance.GetScore();
            UpdateScore();
        }

        if (GameManager.instance.GetLives()!= lastLives)
        {
            lastLives = GameManager.instance.GetLives();
            UpdateLives();
        }

        if (GameManager.instance.GetTotalMeat()!= lastMeatCount)
        {
            lastMeatCount = GameManager.instance.GetTotalMeat();
            UpdateMeatCount();
        }

        // Updates the Text UI Element
        int minutes = (int) GameManager.instance.GetGameTime() / 60;
        int seconds = (int) GameManager.instance.GetGameTime() - (minutes * 60);
        timeText.text = minutes.ToString("#00") + " " + seconds.ToString("#00");
    }

    private void UpdateScore()
    {
        scoreText.text = "Score " + GameManager.instance.GetScore();
    }

    private void UpdateLives()
    {
        switch (GameManager.instance.GetLives()) 
        {
            case 0:
                // lose game
                hpIcon1.SetActive(false);
                hpIcon2.SetActive(false);
                hpIcon3.SetActive(false);
                extraLivesText.gameObject.SetActive(false);
                break;
            case 1:
                // have 1 heart shown
                hpIcon1.SetActive(true);
                hpIcon2.SetActive(false);
                hpIcon3.SetActive(false);
                extraLivesText.gameObject.SetActive(false);
                break;
            case 2:
                // have 2 heart shown
                hpIcon1.SetActive(true);
                hpIcon2.SetActive(true);
                hpIcon3.SetActive(false);
                extraLivesText.gameObject.SetActive(false);
                break;
            case 3:
                // have 3 heart shown
                hpIcon1.SetActive(true);
                hpIcon2.SetActive(true);
                hpIcon3.SetActive(true);
                extraLivesText.gameObject.SetActive(false);
                break;
            default:
                // have 3 heart shown and Extra Lives Text
                extraLivesText.gameObject.SetActive(true);
                extraLivesText.text = "+ " + (GameManager.instance.GetLives() - 3);
                break;
        }
    }

    private void UpdateMeatCount()
    {
        meatCountText.text = "x" + GameManager.instance.GetTotalMeat();
    }
}
