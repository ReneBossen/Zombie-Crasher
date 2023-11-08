using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private Animator cameraAnim;
    private Animator tankAnim;
    private BaseController player;
    private PlayerHealth playerHealth;

    //Buy Values
    [SerializeField] private int BuyAmmoAmount = 1;
    [SerializeField] private int buyHealthAmount = 5;
    [SerializeField] private int buySpeedAmount = 1;
    [SerializeField] private float buyFuelAmount = 5f;
    private void Start()
    {
        cameraAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        tankAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseController>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    public void PlayGame()
    {
        cameraAnim.Play("Slide");
        tankAnim.Play("Tank Move Forward");
    }

    public void BuyAmmo()
    {
        player.BuyAmmo(BuyAmmoAmount);
    }

    public void BuyFuel()
    {
        player.BuyFuel(buyFuelAmount);
    }

    public void BuySpeed()
    {
        player.BuySpeed(buySpeedAmount);

    }

    public void BuyHealth()
    {
        playerHealth.AddMaxHealth(buyHealthAmount);
    }
}
