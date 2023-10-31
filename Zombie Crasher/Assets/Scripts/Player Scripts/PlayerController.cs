using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Rigidbody myBody;

    [SerializeField] private Transform bullet_StartPoint;
    [SerializeField] private GameObject bullet_Prefab;
    [SerializeField] private ParticleSystem shootFX;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
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
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveFast();
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveSlow();
        }

        //Reset to standard movement on button release
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveStraight();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveStraight();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveNormal();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
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
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bullet_Prefab, bullet_StartPoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Move(2000f);

            shootFX.Play();
        }
    }
}