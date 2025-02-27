using TMPro;
using UnityEngine;

// DESIGN PATTERN: Builder Pattern
// Defines an interface for building different parts of the HUD. This interface
// allows for the construction process to be separated from the HUD itself,
// enabling different implementations of the HUD builder without altering the HUD class.

public interface IHUDBuilder
{
    void BuildScoreText(TMP_Text scoreText);
    void BuildExtraLivesText(TMP_Text extraLivesText);
    void BuildTimeText(TMP_Text timeText);
    void BuildMeatCountText(TMP_Text meatCountText);
    void BuildHpIcons(GameObject hp1, GameObject hp2, GameObject hp3);
    void BuildMeatIcon(GameObject meatIcon);
    HUD GetHUD();
}