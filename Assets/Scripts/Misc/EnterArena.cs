using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArena : MonoBehaviour
{
    public AudioSource aus;
    public GameObject boss;
    public Camera mainCamera;
    public float cameraChangeSize = 6.7f;
    public AudioClip bossFightIntro;
    private bool introPlayed;
    private float bossFightIntroDuration = 2;
    public AudioClip bossFightMelody;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        if(introPlayed)
        {
            bossFightIntroDuration -= Time.deltaTime;
            if(bossFightIntroDuration <= 0)
            {
                aus.clip = bossFightMelody;
                aus.Play();
                introPlayed = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            mainCamera.orthographicSize = cameraChangeSize;
            boss.SetActive(true);
            aus.clip = bossFightIntro;
            aus.Play();
            introPlayed = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
