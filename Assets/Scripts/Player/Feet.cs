using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    private Animator anim;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        gameObject.transform.position = player.transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Walking", true);
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Walking", false);
        }
    }
}
