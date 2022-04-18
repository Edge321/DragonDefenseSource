/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// How the soul shop functions (OUTDATED)
/// </summary>
public class OldSoulShop : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        /*int soulPriceNumber = int.Parse(soulPrice.text);

        if (soulPriceNumber > soulTotal)
            SetButtonColors(noMoneyColor);
        else
            SetButtonColors(haveMoneyColor);
    }
    ///<summary>
    ///When a button is pressed, gets the components of the button.
    ///Also sets the soul price and increases upgrade cost
    ///</summary>
    public void ButtonPress(Button button)
    {
        Text textButton = button.GetComponentInChildren<Text>();
        string buttonString = textButton.text.ToLower();

        int soulPriceNumber = int.Parse(soulPrice.text);

        SetSouls(-soulPriceNumber, buttonString, button);
    }
    /// <summary>
    /// Stuff
    /// </summary>
    /// <param name="soulAmount"></param>
    /// <param name="function"></param>
    /// <param name="button"></param>
    public void SetSouls(int soulAmount, string function, Button button)
    {
        int tempSouls = soulTotal;
        soulTotal += soulAmount;

        if (soulTotal < 0) //Checks if player has enough souls
        {
            AudioBehavior.instance.PlayAudio(cantBuySound);
            soulTotal = tempSouls;
        }
        else
        {
            ShopBehavior(function, button);
            soulAmount = Mathf.Clamp(Mathf.Abs(soulAmount) + 1, 0, maxPriceUpgrade); //Price is increased
            soulPrice.text = "" + soulAmount; //Price set in text
            AudioBehavior.instance.PlayAudio(buySound);
        }
    }
    ///<summary>
    ///Lol
    ///</summary>
    private void ShopBehavior(string function, Button button)
    {
        bool disable = false;
        switch (function)
        {
            case "+health":
                player.ChangeHealth(plusHealth);
                break;
            case "+flight speed":
                disable = player.FlightSpeedModifier(plusFlight);
                break;
            case "+attack speed":
                disable = player.AttackDelayModifier(plusAttack);
                break;
            case "+max health":
                disable = player.MaxHealthModifier(plusMaxHealth);
                player.ChangeHealth(plusMaxHealth / 2);
                break;
        }
        soulText.text = "Soul Amount\n" + soulTotal;

        if (disable)
            button.interactable = false;
    }
}*/
