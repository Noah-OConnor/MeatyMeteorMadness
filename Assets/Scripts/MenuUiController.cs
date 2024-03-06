using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUiController : MonoBehaviour
{

    public void PlayButton()
    {
        GameManager.instance.ResetGame();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
