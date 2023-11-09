using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{

    //Upgradables
    public float bonusSpeed;
    public int maxAmmo;
    public float maxFuel;

    protected int ammo;
    protected float fuel;

    public Vector3 speed;
    public float x_Speed = 8f, z_Speed = 10f;
    public float accelerated = 15f, deaccelerated = 5f;

    protected float rotationSpeed = 10f;
    protected float maxAngle = 10f;

    public float low_Sound_Pitch, normal_Sound_Pitch, high_Sound_Pitch;

    public AudioClip engine_On_Sound, engine_Off_Sound;
    private bool is_Slow;

    [HideInInspector] public AudioSource soundManager;


    private void Start()
    {
        soundManager = GetComponent<AudioSource>();

        speed = new Vector3(0.0f, 0.0f, z_Speed);

        //Assign from local DB
        bonusSpeed = ValuesToKeepBetweenScenes.bonusSpeed;
        maxAmmo = ValuesToKeepBetweenScenes.maxAmmo;
        maxFuel = ValuesToKeepBetweenScenes.maxFuel;
        ammo = maxAmmo;
        fuel = maxFuel;

        SetCorrectSpeed();

        Debug.Log(bonusSpeed);
        Debug.Log("Current speed values at start " + deaccelerated + " " + accelerated + " " + z_Speed);
    }

    protected void MoveLeft()
    {
        speed = new Vector3(-x_Speed / 2f, 0f, speed.z);
    }
    protected void MoveRight()
    {
        speed = new Vector3(x_Speed / 2f, 0f, speed.z);
    }
    protected void MoveStraight()
    {
        speed = new Vector3(0f, 0f, z_Speed);
    }
    protected void MoveNormal()
    {
        if (Time.timeScale != 0)
        {
            if (is_Slow)
            {
                is_Slow = false;
                soundManager.Stop();
                soundManager.clip = engine_On_Sound;
                soundManager.volume = 0.3f;
                soundManager.Play();
            }
            speed = new Vector3(speed.x, 0f, speed.z);
        }
    }

    protected void MoveSlow()
    {
        if (Time.timeScale != 0)
        {
            if (!is_Slow)
            {
                is_Slow = true;
                soundManager.Stop();
                soundManager.clip = engine_Off_Sound;
                soundManager.volume = 0.3f;
                soundManager.Play();
            }
            speed = new Vector3(speed.x, 0f, deaccelerated);
        }
    }

    protected void MoveFast()
    {
        speed = new Vector3(speed.x, 0f, accelerated);
    }

    private void SetCorrectSpeed()
    {
        deaccelerated += bonusSpeed;
        accelerated += bonusSpeed;
        z_Speed += bonusSpeed;
        Debug.Log("Setting correct speed");
    }

    public void BuyAmmo(int amount)
    {
        maxAmmo += amount;
        ValuesToKeepBetweenScenes.maxAmmo = maxAmmo;
    }

    public void BuyFuel(float amount)
    {
        maxFuel += amount;
        ValuesToKeepBetweenScenes.maxFuel = maxFuel;
    }

    public void BuySpeed(float amount)
    {
        bonusSpeed += amount;
        ValuesToKeepBetweenScenes.bonusSpeed = bonusSpeed;
    }
}