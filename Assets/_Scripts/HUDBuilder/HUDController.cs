using TMPro;
using UnityEngine;

// DESIGN PATTERN: Builder Pattern
// The controller class uses the builder and director to construct the HUD.
// It initializes the HUD using the builder pattern and updates the HUD elements
// based on the game state.

public class HUDController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text extraLivesText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text meatCountText;

    [SerializeField] private GameObject hpIcon1;
    [SerializeField] private GameObject hpIcon2;
    [SerializeField] private GameObject hpIcon3;
    [SerializeField] private GameObject meatIcon;

    private HUD hud;
    private int lastScore;
    private int lastLives;
    private int lastMeatCount;

    private void Start()
    {
        // Use Builder to create HUD
        IHUDBuilder hudBuilder = new HUDBuilder();
        HUDDirector director = new HUDDirector(hudBuilder);
        hud = director.ConstructHUD(scoreText, extraLivesText, timeText, meatCountText,
                                    hpIcon1, hpIcon2, hpIcon3, meatIcon);

        hud.UpdateScore(lastScore);
    }

    private void Update()
    {
        if (_GameManager.instance.GetScore() != lastScore)
        {
            lastScore = _GameManager.instance.GetScore();
            hud.UpdateScore(lastScore);
        }

        if (_GameManager.instance.GetLives() != lastLives)
        {
            lastLives = _GameManager.instance.GetLives();
            hud.UpdateLives(lastLives);
        }

        if (_GameManager.instance.GetTotalMeat() != lastMeatCount)
        {
            lastMeatCount = _GameManager.instance.GetTotalMeat();
            hud.UpdateMeatCount(lastMeatCount);
        }

        hud.UpdateTime(_GameManager.instance.GetGameTime());
    }
}