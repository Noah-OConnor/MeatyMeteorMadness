using TMPro;
using UnityEngine;

// DESIGN PATTERN: Builder Pattern
// The director class is responsible for constructing the HUD using the builder interface.
// It directs the building process by calling the appropriate methods on the builder
// to create a fully constructed HUD object.

public class HUDDirector
{
    private IHUDBuilder builder;

    public HUDDirector(IHUDBuilder builder)
    {
        this.builder = builder;
    }

    public HUD ConstructHUD(TMP_Text score, TMP_Text lives, TMP_Text time, TMP_Text meatCount,
                            GameObject hp1, GameObject hp2, GameObject hp3, GameObject meatIcon)
    {
        builder.BuildScoreText(score);
        builder.BuildExtraLivesText(lives);
        builder.BuildTimeText(time);
        builder.BuildMeatCountText(meatCount);
        builder.BuildHpIcons(hp1, hp2, hp3);
        builder.BuildMeatIcon(meatIcon);
        return builder.GetHUD();
    }
}