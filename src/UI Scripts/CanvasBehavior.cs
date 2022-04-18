using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalGameMechanics;

public class CanvasBehavior : MonoBehaviour
{
    public GameObject titleScreenCanvas;
    public GameObject difficultyCanvas;
    public GameObject gameUICanvas;
    public GameObject gameOverCanvas;
    public GameObject controlsCanvas;
    public GameObject settingsCanvas;
    public GameObject creditsCanvas;

    private float gameOverTime = 5.0f;

    /// <summary>
    /// Disables title screen canvas and enables difficulty canvas
    /// </summary>
    public void StartGame()
    {
        titleScreenCanvas.SetActive(false);
        difficultyCanvas.SetActive(true);
    }
    /// <summary>
    /// Sets the difficulty of game, enables game UI, and enables player to move
    /// </summary>
    /// <param name="difficulty">Sets difficulty of game, 0 - 2</param>
    public void ChooseDifficulty(int difficulty) {
        switch(difficulty)
        {
            case 0:
                GameBehaviors.Difficulty = Difficulties.EASY;
                break;
            case 1:
                GameBehaviors.Difficulty = Difficulties.NORMAL;
                break;
            case 2:
                GameBehaviors.Difficulty = Difficulties.HARD;
                break;
            default:
                GameBehaviors.Difficulty = Difficulties.NORMAL;
                break;
        }

        difficultyCanvas.SetActive(false);
        gameUICanvas.SetActive(true);

        GameBehaviors.Moveable = true;
    }
    /// <summary>
    /// Sets certain canvases active and inactive after a game over
    /// </summary>
    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        gameUICanvas.SetActive(false);
        Invoke("StartOver", gameOverTime);
    }
    /// <summary>
    /// Goes back to the title screen and sets <c>GameBehaviors.Reset</c> to <c>true</c>
    /// </summary>
    private void StartOver()
    {
        titleScreenCanvas.SetActive(true);
        gameUICanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        GameBehaviors.Reset = true;
    }
    /// <summary>
    /// Shows the settings canvas
    /// </summary>
    public void ShowSettings()
    {
        titleScreenCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }
    /// <summary>
    /// Shows the credits canvas
    /// </summary>
    public void ShowCredits()
    {
        titleScreenCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }
    /// <summary>
    /// Shows the controls canvas
    /// </summary>
    public void ShowControls()
    {
        titleScreenCanvas.SetActive(false);
        controlsCanvas.SetActive(true);
    }
    /// <summary>
    /// Disables whatever canvas the player was on and goes back to the main menu
    /// </summary>
    public void BackButton()
    {
        creditsCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        titleScreenCanvas.SetActive(true);
    }
    /// <summary>
    /// Exits the game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
