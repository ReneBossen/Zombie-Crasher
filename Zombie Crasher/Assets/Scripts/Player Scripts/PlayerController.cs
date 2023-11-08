using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    private Rigidbody myBody;
    private Animator shootSliderAnim;

    [SerializeField] private Transform bullet_StartPoint;
    [SerializeField] private GameObject bullet_Prefab;
    [SerializeField] private ParticleSystem shootFX;
    [HideInInspector] public bool canShoot;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        shootSliderAnim = GameObject.Find("Fire Bar").GetComponent<Animator>();

        canShoot = true;
    }

    private void Update()
    {
        ControllMovement();
        ChangeRotation();

        ShootingControl();
    }

    private void FixedUpdate()
    {

        MoveTank();
    }

    private void MoveTank()
    {
        myBody.MovePosition(myBody.position + speed * Time.deltaTime);
    }

    private void ControllMovement()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            //Debug.Log("Højre");
            MoveRight();
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            //Debug.Log("Venstre");
            MoveLeft();
        }
        else
        {
            //Debug.Log("Kører lige frem");
            MoveStraight();
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            //Debug.Log("Frem");
            MoveFast();
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            //Debug.Log("Tilbage");
            MoveSlow();
        }
        else
        {
            //Debug.Log("Kører normal hastighed");
            MoveNormal();
        }
    }

    private void ChangeRotation()
    {
        Quaternion target_Rotation;

        if (speed.x > 0)
        {
            target_Rotation = Quaternion.Euler(0f, maxAngle, 0f);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else if (speed.x < 0)
        {
            target_Rotation = Quaternion.Euler(0f, -maxAngle, 0f);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, -maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else
        {
            target_Rotation = Quaternion.Euler(0f, 0f, 0f);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * rotationSpeed);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, target_Rotation, Time.deltaTime * 100f);
    }

    public void ShootingControl()
    {
        if (Time.timeScale != 0)
        {
            if (ammo > 0)
            {
                if (canShoot)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        GameObject bullet = Instantiate(bullet_Prefab, bullet_StartPoint.position, Quaternion.identity);
                        bullet.GetComponent<Bullet>().Move(2000f);
                        shootFX.Play();

                        ammo--;
                        canShoot = false;
                        shootSliderAnim.Play("Fill");
                    }
                }
            }
        }
    }
}