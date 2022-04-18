using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBehavior : MonoBehaviour
{
    public static HealthBehavior instance { get; private set; }

    public Image mask;
    private float originalSize;

    void Awake()
    {
        instance = this;
        originalSize = mask.rectTransform.rect.width;
    }
    /// <summary>
    /// Method that changes the size of the health bar's mask
    /// </summary>
    /// <param name="value">Sets the horizontal size of the red health bar</param>
    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 
                                                    originalSize * value);
    }
}
