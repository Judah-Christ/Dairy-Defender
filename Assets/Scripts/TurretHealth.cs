using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretHealth : MonoBehaviour
{
    public Slider healthBar;
    public Image fillImage;

    public float maxHealth = 100f;
    public float currentHealth;

    public Color highHealthColor = Color.green;
    public Color mediumHealthColor = Color.yellow;
    public Color lowHealthColor = Color.red;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        UpdateHealthBarColor();
    }

    void Update()
    {
        healthBar.value = currentHealth;
        UpdateHealthBarColor();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        UpdateHealthBarColor();
    }

    void UpdateHealthBarColor()
    {
        float healthPercentage = currentHealth / maxHealth;

        if (healthPercentage > 0.7f)
        {
            fillImage.color = highHealthColor;
        }
        else if (healthPercentage > 0.3f)
        {
            fillImage.color = mediumHealthColor;
        }
        else
        {
            fillImage.color = lowHealthColor;
        }
    }
}
