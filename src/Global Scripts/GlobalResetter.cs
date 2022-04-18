using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalGameMechanics;

/// <summary>
/// Class that handles what is necessary when the game is over
/// </summary>
public class GlobalResetter : MonoBehaviour
{
    public PlayerController player;
    public SoulShopBehavior soulShop;
    public SpawnEnemies spawnEnemies;
    public CanvasBehavior canvasBehavior;
    // Update is called once per frame
    void Update()
    {
        if (GameBehaviors.GameOver)
        {
            ResetAll();
        }
        if (GameBehaviors.Reset)
        {
            RestartGame();
        }
            
    }
    /// <summary>
    /// Resets what is required after a game over
    /// </summary>
    private void ResetAll()
    {
        player.ResetStats();
        soulShop.ResetSouls();
        soulShop.ResetPrices();
        spawnEnemies.ResetSpawnTimers();
        canvasBehavior.GameOver();
        GameBehaviors.GameOver = false;
    }
    /// <summary>
    /// After restarting the game, makes <c>GameBehaviors.Reset false</c> and resumes BGM.
    /// </summary>
    private void RestartGame()
    {
        AudioBehavior.instance.PlayBGM();
        GameBehaviors.Reset = false;
    }
}
