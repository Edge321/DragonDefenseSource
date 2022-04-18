using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTextBehavior : MonoBehaviour
{
    public static HealthTextBehavior instance { get; private set; }

    private Text text;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        text = GetComponent<Text>();
    }

    // FixedUpdate is called once per fixed frame
    public void SetHealthText(float currentHealth, float maxHealth)
    {
        text.text = "Health\n"
                    + currentHealth + "/" + maxHealth;
    }
}
