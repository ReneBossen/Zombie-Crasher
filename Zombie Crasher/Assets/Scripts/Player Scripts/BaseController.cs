using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
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
        Debug.Log(soundManager);
        speed = new Vector3(0.0f, 0.0f, z_Speed);
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
}
