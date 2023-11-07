using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private Animator cameraAnim;
    private Animator tankAnim;
    private void Start()
    {
        cameraAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        tankAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    public void PlayGame()
    {
        cameraAnim.Play("Slide");
        tankAnim.Play("Tank Move Forward");
    }
}