using TMPro;
using UnityEngine;

// DESIGN PATTERN: Builder Pattern, Observer Pattern
// The controller class uses the builder and director to construct the HUD.
// It initializes the HUD using the builder pattern and updates the HUD elements
// based on the game state.
// Implements the Observer pattern by subscirbing to gamemanger events and calling
// functions based on that

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

    private void OnEnable()
    {
        if (GameManager.instance == null || hud == null) return;
        GameManager.OnScoreUpdated += hud.UpdateScore;
        GameManager.OnLivesUpdated += hud.UpdateLives;
        GameManager.OnTimeUpdated += hud.UpdateTime;
        GameManager.OnMeatCountUpdated += hud.UpdateMeatCount;
    }

    private void OnDisable()
    {
        if (GameManager.instance == null || hud == null) return;
        GameManager.OnScoreUpdated -= hud.UpdateScore;
        GameManager.OnLivesUpdated -= hud.UpdateLives;
        GameManager.OnTimeUpdated -= hud.UpdateTime;
        GameManager.OnMeatCountUpdated -= hud.UpdateMeatCount;
    }

    private void Awake()
    {
        // Use Builder to create HUD
        IHUDBuilder hudBuilder = new HUDBuilder();
        HUDDirector director = new HUDDirector(hudBuilder);
        hud = director.ConstructHUD(scoreText, extraLivesText, timeText, meatCountText,
                                    hpIcon1, hpIcon2, hpIcon3, meatIcon);

        hud.UpdateScore(0);
    }
}