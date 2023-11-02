using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float timer = 3f;

    private void Start()
    {
        Invoke("DeactivateGameObject", timer);
    }

    private void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}
