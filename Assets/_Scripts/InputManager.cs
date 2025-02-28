using UnityEngine;

// DESIGN PATTERN - Facade
// Implements the Facade pattern by centralizing input handling for the entire game.
// This class provides a single interface for processing player movement, pausing, and quitting.
public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    private _PlayerController playerController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerController = FindFirstObjectByType<_PlayerController>();
    }

    private void Update()
    {
        HandleGameControls();
        HandlePlayerMovement();
    }

    private void HandleGameControls()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    private void HandlePlayerMovement()
    {
        if (_GameManager.instance.lose) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        playerController.HandleMovement(horizontalInput);
    }

    public void TogglePause()
    {
        Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
