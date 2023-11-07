using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private GameObject bloodFxPrefab;
    [SerializeField] private float speed;

    private Rigidbody myBody;
    private bool isAlive;

    void Start()
    {
        myBody = GetComponent<Rigidbody>();

        speed = Random.Range(1f, 5f);

        isAlive = true;
    }

    void Update()
    {
        if (isAlive)
        {
            myBody.velocity = new Vector3(0f, 0f, -speed);
        }

        if (transform.position.y < -10f)
        {
            gameObject.SetActive(false);
        }
    }

    private void Die()
    {
        isAlive = false;

        myBody.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<Animator>().Play("Idle");

        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        transform.localScale = new Vector3(1f, 1f, 0.2f);
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }

    private void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Bullet")
        {
            Instantiate(bloodFxPrefab, transform.position, Quaternion.identity);

            Invoke("DeactivateGameObject", 3f);

            GameplayController.instance.IncreaseScore();

            Die();
        }
    }
}