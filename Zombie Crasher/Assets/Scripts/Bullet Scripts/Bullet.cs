using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    public void Move(float speed)
    {
        myBody.AddForce(transform.forward.normalized * speed);
        Invoke("Deactivate", 1f);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Zombie")
        {
            gameObject.SetActive(false);
        }
    }
}
