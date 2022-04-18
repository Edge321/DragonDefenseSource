using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPricing : MonoBehaviour
{
    public int[] prices = new int[10];

    public GameObject shopButton;

    public Color unbuyableColor;
    public Color buyableColor;

    private int priceIndex = 0;
    private int maxIndex;

    private Button button;

    private Text text;

    private string maxString = "MAX";

    private Image image;
    private void Awake()
    {
        text = GetComponent<Text>();
        maxIndex = prices.Length - 1;
        image = shopButton.GetComponent<Image>();
        button = shopButton.GetComponent<Button>();
    }
    private void Update()
    {
        if(button.interactable)
            text.text = prices[priceIndex].ToString();
    }
    /// <summary>
    /// Increases price of an upgrade
    /// </summary>
    public void IncreasePrice()
    {
        priceIndex = Mathf.Clamp(priceIndex + 1, 0, maxIndex);
    }
    /// <summary>
    /// Checks if an upgrade is buyable. Sets the button's color depending on if it is buyable
    /// </summary>
    /// <param name="soulTotal">Total amount of souls player has</param>
    public void Buyable(int soulTotal)
    {
        if (prices[priceIndex] <= soulTotal && button.interactable)
            image.color = buyableColor;
        else
            image.color = unbuyableColor;
    }
    /// <summary>
    /// Resets the price on the button
    /// </summary>
    public void ResetPrice()
    {
        priceIndex = 0;
        button.interactable = true;
    }
    /// <summary>
    /// Changes button to say "MAX" and makes it un-interactable
    /// </summary>
    public void MaxxedPrice()
    {
        text.text = maxString;
        button.interactable = false;
    }
}
