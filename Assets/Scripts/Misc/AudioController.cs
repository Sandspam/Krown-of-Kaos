using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource aus;

    // Start is called before the first frame update
    void Start()
    {
        aus = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        aus.pitch = Random.Range(0.9f, 1.1f);
    }
}
