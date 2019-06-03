using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource aus;
    public AudioClip pickUpSound;

    void Start()
    {
        aus = gameObject.GetComponent<AudioSource>();
    }

    public void PlayPickUpSound ()
    {
        aus.clip = pickUpSound;
        aus.Play();
    }
}
