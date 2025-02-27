using TMPro;
using UnityEngine;

// DESIGN PATTERN: Builder Pattern
// Implements the IHUDBuilder interface to construct the HUD. This class provides
// the concrete implementation of the builder methods to set up each part of the HUD.

public class HUDBuilder : IHUDBuilder
{
    private HUD hud = new HUD();

    public void BuildScoreText(TMP_Text scoreText)
    {
        hud.ScoreText = scoreText;
    }

    public void BuildExtraLivesText(TMP_Text extraLivesText)
    {
        hud.ExtraLivesText = extraLivesText;
    }

    public void BuildTimeText(TMP_Text timeText)
    {
        hud.TimeText = timeText;
    }

    public void BuildMeatCountText(TMP_Text meatCountText)
    {
        hud.MeatCountText = meatCountText;
    }

    public void BuildHpIcons(GameObject hp1, GameObject hp2, GameObject hp3)
    {
        hud.HpIcon1 = hp1;
        hud.HpIcon2 = hp2;
        hud.HpIcon3 = hp3;
    }

    public void BuildMeatIcon(GameObject meatIcon)
    {
        hud.MeatIcon = meatIcon;
    }

    public HUD GetHUD()
    {
        return hud;
    }
}