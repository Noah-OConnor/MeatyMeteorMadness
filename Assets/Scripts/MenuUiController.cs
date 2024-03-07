using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuUiController : MonoBehaviour
{
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private TMP_Text titleText;

    [SerializeField] private GameObject bestStats;
    [SerializeField] private TMP_Text bestScore;
    [SerializeField] private TMP_Text bestTime;
    [SerializeField] private TMP_Text bestMeat;

    [SerializeField] private GameObject thisRunStats;
    [SerializeField] private TMP_Text thisScore;
    [SerializeField] private TMP_Text thisTime;
    [SerializeField] private TMP_Text thisMeat;

    public void SetUp()
    {
        EnableMainScreen();
        ShowBests();
        titleText.text = "DINO GAME";
        thisRunStats.SetActive(false);
    }

    public void Lose()
    {
        titleText.text = "EXTINCTION";

        thisScore.text = GameManager.instance.GetScore().ToString();
        thisMeat.text = GameManager.instance.GetMeat().ToString();

        int minutes = (int)GameManager.instance.GetGameTime() / 60;
        int seconds = (int)GameManager.instance.GetGameTime() - (minutes * 60);
        thisTime.text = minutes.ToString("#00") + ":" + seconds.ToString("#00");

        if (PlayerPrefs.HasKey("BestScore"))
        {
            if (GameManager.instance.GetScore() > PlayerPrefs.GetInt("BestScore"))
            {
                PlayerPrefs.SetInt("BestScore", GameManager.instance.GetScore());
            }
            if (GameManager.instance.GetMeat() > PlayerPrefs.GetInt("BestMeat"))
            {
                PlayerPrefs.SetInt("BestMeat", GameManager.instance.GetMeat());
            }
            if (GameManager.instance.GetGameTime() > PlayerPrefs.GetInt("BestTime"))
            {
                PlayerPrefs.SetInt("BestTime", (int)GameManager.instance.GetGameTime());
            }
        }
        else
        {
            PlayerPrefs.SetInt("BestMeat", GameManager.instance.GetMeat());
            PlayerPrefs.SetInt("BestScore", GameManager.instance.GetScore());
            PlayerPrefs.SetInt("BestTime", (int)GameManager.instance.GetGameTime());
        }
        ShowBests();
    }

    private void ShowBests()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestStats.SetActive(true);
            bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
            bestMeat.text = PlayerPrefs.GetInt("BestMeat").ToString();

            int minutes = PlayerPrefs.GetInt("BestTime") / 60;
            int seconds = PlayerPrefs.GetInt("BestTime") - (minutes * 60);
            bestTime.text = minutes.ToString("#00") + ":" + seconds.ToString("#00");
        }
        else 
        {
            bestScore.text = "0";
            bestMeat.text = "0";
            bestTime.text = "00:00";

            bestStats.SetActive(false);
        }
    }

    public void PlayButton()
    {
        thisRunStats.SetActive(true);
        GameManager.instance.ResetGame();
    }

    public void QuitButton()
    {
        Application.Quit();
        print("Quit Game");
    }

    public void DisableMainScreen()
    {
        mainScreen.SetActive(false);
    }

    public void EnableMainScreen()
    {
        mainScreen.SetActive(true);
    }
}
