using TMPro;
using UnityEngine;

// DESIGN PATTERN: Builder Pattern
// Represents the product created by the builder. This class contains the HUD elements
// and provides methods to update them. The HUD is constructed using the builder pattern
// to separate the construction process from the HUD's functionality.

public class HUD
{
    public TMP_Text ScoreText { get; set; }
    public TMP_Text ExtraLivesText { get; set; }
    public TMP_Text TimeText { get; set; }
    public TMP_Text MeatCountText { get; set; }

    public GameObject HpIcon1 { get; set; }
    public GameObject HpIcon2 { get; set; }
    public GameObject HpIcon3 { get; set; }
    public GameObject MeatIcon { get; set; }

    public void UpdateScore(int score)
    {
        ScoreText.text = "Score " + score;
    }

    public void UpdateLives(int lives)
    {
        HpIcon1.SetActive(lives >= 1);
        HpIcon2.SetActive(lives >= 2);
        HpIcon3.SetActive(lives >= 3);

        if (lives > 3)
        {
            ExtraLivesText.gameObject.SetActive(true);
            ExtraLivesText.text = "+" + (lives - 3);
        }
        else
        {
            ExtraLivesText.gameObject.SetActive(false);
        }
    }

    public void UpdateMeatCount(int meatCount)
    {
        MeatCountText.text = "x" + meatCount;
    }

    public void UpdateTime(int gameTime)
    {
        int minutes = gameTime / 60;
        int seconds = gameTime - (minutes * 60);
        TimeText.text = minutes.ToString("00") + " " + seconds.ToString("00");
    }
}