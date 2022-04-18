using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalGameMechanics;

public class PlayerStatsBehavior : MonoBehaviour
{
    public PlayerController player;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        SetStatText();
    }
    /// <summary>
    /// Sets the text with the current player stats
    /// </summary>
    private void SetStatText()
    {
        text.text = "Your Stats\n" +
                    "--------------\n" +
                    "-Max Health-\n" + player.ModifiedMaxHealthGetSet +
                    "\n-Flight Speed-\n" + player.FlightSpeedGetSet.ToString("F2") + " m/s" +
                    "\n-Attack Delay-\n" + player.AttackDelayGetSet.ToString("F2") + " second" +
                    "\n-Score-\n" + GameBehaviors.GameScore +
                    "\n-High-Score-\n" + GameBehaviors.HighScore;
    }
}
