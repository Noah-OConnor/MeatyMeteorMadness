using UnityEngine;
using TMPro;

// DESIGN PATTERNS - Memento
// Implements the Memento pattern to save and restore game state from a file.

public class MenuUiController : MonoBehaviour
{
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject stats;

    [SerializeField] private GameObject bestStats;
    [SerializeField] private TMP_Text bestScore;
    [SerializeField] private TMP_Text bestTime;
    [SerializeField] private TMP_Text bestMeat;

    [SerializeField] private GameObject thisRunStats;
    [SerializeField] private TMP_Text thisScore;
    [SerializeField] private TMP_Text thisTime;
    [SerializeField] private TMP_Text thisMeat;

    private GameMemento savedData;

    public void SetUp()
    {
        savedData = GameMemento.Load();
        AudioManager.instance.MainMenu();
        EnableMainScreen();
        DisableLoseScreen();
        ShowBests();
        thisRunStats.SetActive(false);
    }

    public void Lose()
    {
        //titleText.text = "EXTINCTION";

        thisScore.text = GameManager.instance.GetScore().ToString();
        thisMeat.text = GameManager.instance.GetTotalMeat().ToString();

        int minutes = (int)GameManager.instance.GetGameTime() / 60;
        int seconds = (int)GameManager.instance.GetGameTime() - (minutes * 60);
        thisTime.text = minutes.ToString("#00") + " " + seconds.ToString("#00");

        // Update the best stats only if the current run is better
        if (GameManager.instance.GetScore() > savedData.BestScore)
        {
            savedData.BestScore = GameManager.instance.GetScore();
        }
        if (GameManager.instance.GetTotalMeat() > savedData.BestMeat)
        {
            savedData.BestMeat = GameManager.instance.GetTotalMeat();
        }
        if (GameManager.instance.GetGameTime() > savedData.BestTime)
        {
            savedData.BestTime = (int)GameManager.instance.GetGameTime();
        }

        // Save the updated data
        GameMemento.Save(savedData);

        ShowBests();
    }

    private void ShowBests()
    {
        bestStats.SetActive(savedData.BestScore > 0);
        bestScore.text = savedData.BestScore.ToString();
        bestMeat.text = savedData.BestMeat.ToString();

        int minutes = savedData.BestTime / 60;
        int seconds = savedData.BestTime - (minutes * 60);
        bestTime.text = minutes.ToString("#00") + " " + seconds.ToString("#00");
    }

    public void PlayButton()
    {
        thisRunStats.SetActive(true);
        GameManager.instance.ResetGame();
    }

    public void QuitButton()
    {
        Application.Quit();
        //print("Quit Game");
    }

    public void MainMenuButton()
    {
        DisableLoseScreen();
        EnableMainScreen();
        AudioManager.instance.MainMenu();
    }

    public void DisableMainScreen() => mainScreen.SetActive(false);
    public void EnableMainScreen() => mainScreen.SetActive(true);
    public void DisableLoseScreen() => loseScreen.SetActive(false);
    public void EnableLoseScreen() => loseScreen.SetActive(true);
    public void DisableStatsScreen() => stats.SetActive(false);
    public void EnableStatsScreen() => stats.SetActive(true);
}
