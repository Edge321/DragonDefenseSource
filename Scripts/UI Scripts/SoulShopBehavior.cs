using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulShopBehavior : MonoBehaviour
{
    public static SoulShopBehavior instance { get; private set; }

    public PlayerController player;

    public AudioClip cantBuySound;
    public AudioClip buySound;

    public Text soulText;

    public ButtonPricing[] buttonPricing;

    private int soulTotal = 0;
    private int plusHealth = 100;
    private int plusMaxHealth = 50;

    private float plusFlight = 0.03f;
    private float plusAttack = 0.04f;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        soulText.text = "Soul Amount\n" + soulTotal;
    }
    private void Update()
    {
        CheckPricing();
    }
    /// <summary>
    /// Checks if an upgrade is buyable by iterating through the buttons
    /// </summary>
    private void CheckPricing()
    {
        for (int i = 0; i < buttonPricing.Length; i++)
        {
            buttonPricing[i].Buyable(soulTotal);
        }
    }
    /// <summary>
    /// Adds souls to the total amount
    /// </summary>
    /// <param name="soulAmount">Souls to be added to total</param>
    public void AddSouls(int soulAmount)
    {
        soulTotal += soulAmount;
        soulText.text = "Soul Amount\n" + soulTotal;
    }
    /// <summary>
    /// Reset stats for souls and upgrade price
    /// </summary>
    public void ResetSouls()
    {
        soulTotal = 0;
        soulText.text = "Soul Amount\n" + soulTotal;
    }
    /// <summary>
    /// Resets the prices of upgrades for each button
    /// </summary>
    public void ResetPrices()
    {
        for (int i = 0; i < buttonPricing.Length; i++)
        {
            buttonPricing[i].ResetPrice();
        }
    }
    /// <summary>
    /// Adds more health to the player
    /// </summary>
    /// <param name="text">Text that will be changed</param>
    public void MoreHealth(Text text)
    {
        ButtonPricing buttonPricing = text.GetComponent<ButtonPricing>();
        int priceNumber = int.Parse(text.text);
        if (player.HealthEqualsMax()) //Prevents the player from wasting their money on healing if they are full health
            priceNumber = soulTotal + 1;
        bool buy = Buyable(priceNumber);

        if (buy)
        {
            buttonPricing.IncreasePrice();
            player.ChangeHealth(plusHealth);
        }
    }
    /// <summary>
    /// Adds more flight speed to the player
    /// </summary>
    /// <param name="text">Text that will be changed</param>
    public void MoreSpeed(Text text)
    {
        ButtonPricing buttonPricing = text.GetComponent<ButtonPricing>();
        int priceNumber = int.Parse(text.text);
        bool buy = Buyable(priceNumber);
        bool maxed = false;

        if (buy)
        {
            buttonPricing.IncreasePrice();
            maxed = player.FlightSpeedModifier(plusFlight);
        }

        MaxedUpgrade(buttonPricing, maxed);
    }
    /// <summary>
    /// Adds more max health to the player
    /// </summary>
    /// <param name="text">Text that will be changed</param>
    public void MoreMaxHealth(Text text)
    {
        ButtonPricing buttonPricing = text.GetComponent<ButtonPricing>();
        int priceNumber = int.Parse(text.text);
        bool buy = Buyable(priceNumber);
        bool maxed = false;

        if (buy)
        {
            buttonPricing.IncreasePrice();
            maxed = player.MaxHealthModifier(plusMaxHealth);
            player.ChangeHealth(plusMaxHealth / 2);
        }

        MaxedUpgrade(buttonPricing, maxed);
    }
    /// <summary>
    /// Raises attack speed of player
    /// </summary>
    /// <param name="text">Text that will be changed</param>
    public void MoreAttack(Text text)
    {
        ButtonPricing buttonPricing = text.GetComponent<ButtonPricing>();
        int priceNumber = int.Parse(text.text);
        bool buy = Buyable(priceNumber);
        bool maxed = false;

        if (buy)
        {
            buttonPricing.IncreasePrice();
            maxed = player.AttackDelayModifier(plusAttack);
        }

        MaxedUpgrade(buttonPricing, maxed);
    }
    /// <summary>
    /// Checks if an upgrade is buyable
    /// </summary>
    /// <param name="priceNumber">The price that is checked with the total souls</param>
    /// <returns>A bool that if an upgrade is buyable</returns>
    private bool Buyable(int priceNumber)
    {
        int tempSouls = soulTotal;
        soulTotal -= priceNumber;

        if (soulTotal < 0)
        {
            soulTotal = tempSouls;
            AudioBehavior.instance.PlayAudio(cantBuySound);
            return false;
        }
        else
        {
            soulText.text = "Soul Amount\n" + soulTotal;
            AudioBehavior.instance.PlayAudio(buySound);
            return true;
        }
    }
    /// <summary>
    /// If an upgrade is maxxed, makes the associated upgrade unbuyable
    /// </summary>
    /// <param name="buttonPricing">Script that makes the button maxxed out</param>
    /// <param name="maxed">Checks if a button is maxxed out</param>
    private void MaxedUpgrade(ButtonPricing buttonPricing, bool maxed)
    {
        if (maxed)
        {
            buttonPricing.MaxxedPrice();
        }
    }
}
