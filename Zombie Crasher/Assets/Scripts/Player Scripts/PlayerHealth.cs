using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int healthValue;
    private Slider health_Slider;
    private GameObject UI_Holder;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = ValuesToKeepBetweenScenes.maxHealth;
        //Debug.Log("Currenct maxHealth: " + maxHealth);
        //Debug.Log("Static maxHealth: " + ValuesToKeepBetweenScenes.maxHealth);
        health_Slider = GameObject.Find("Health Bar").GetComponent<Slider>();

        healthValue = maxHealth;

        health_Slider.value = maxHealth;

        UI_Holder = GameObject.Find("UI Holder");
    }

    public void ApplyDamage(int damageAmount)
    {
        healthValue -= damageAmount;

        if (healthValue < 0)
        {
            healthValue = 0;
        }

        health_Slider.value = healthValue;

        if (healthValue == 0)
        {
            UI_Holder.SetActive(false);
            GameplayController.instance.Gameover();
        }
    }

    public void RestartGame()
    {
        healthValue = maxHealth;
        health_Slider.maxValue = healthValue;
        UI_Holder.SetActive(true);
    }

    public void AddMaxHealth(int amount)
    {
        maxHealth += amount;
        health_Slider.maxValue = healthValue;
        ValuesToKeepBetweenScenes.maxHealth = maxHealth;
    }
}
