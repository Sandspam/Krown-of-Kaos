using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Vector3 mousePos;
    [HideInInspector] public bool knockedInTheAir;
    public float moveSpeed;
    public float sprintSpeed;
    public float knockedUpSpeed;
    public bool canMove;
    private GameObject knockBackPoint;
    private Animator anim;
    private HotbarManager hbM;
    private Rigidbody2D rb;

    private void Start()
    {
        Time.timeScale = 1f;
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        hbM = GameObject.Find("HotbarManager").GetComponent<HotbarManager>();
        anim = gameObject.GetComponent<Animator>();
        knockBackPoint = GameObject.Find("PlayerReturnPoint");
    }

    private void Update()
    {
        if(knockedInTheAir)
        {
            transform.position = Vector2.MoveTowards(transform.position, knockBackPoint.transform.position, knockedUpSpeed);
            /*if (transform.position.x == knockBackPoint.transform.position.x && transform.position.y == knockBackPoint.transform.position.y)
            {
                knockedInTheAir = false;
                StopKnockedInAir();
            }*/
        }
        rb.velocity = Vector3.zero;
        Movement();
        /*
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddForce(Vector2.left * moveSpeed);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce(Vector2.right * moveSpeed);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(Vector2.up * moveSpeed);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                rb.AddForce(Vector2.down * moveSpeed);
            }
            else
            {
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddForce(Vector2.left * sprintSpeed);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce(Vector2.right * sprintSpeed);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(Vector2.up * sprintSpeed);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                rb.AddForce(Vector2.down * sprintSpeed);
            }
            else
            {
                rb.velocity = new Vector3(0, 0, 0);
            }
        }*/

        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Walking", false);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Walking", true);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SwordSwing();
        }

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            gameObject.GetComponent<SpellManager>().Fireball();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            hbM.UseItem(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            hbM.UseItem(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hbM.UseItem(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            hbM.UseItem(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            hbM.UseItem(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            hbM.UseItem(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            hbM.UseItem(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            hbM.UseItem(7);
        }

        //Tracks mouse position on the screen
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Rotates the player towards the mouse
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }

    public void SwordSwing ()
    {
        anim.SetBool("Swinging", true);
    }

    public void StopSwing()
    {
        anim.SetBool("Swinging", false);
    }

    void Movement ()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = Vector3.left * moveSpeed;
            }

            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = Vector3.up * moveSpeed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = Vector3.down * moveSpeed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = Vector3.right * moveSpeed;
            }

            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector3(1, 1, 0) * moveSpeed;
            }
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector3(1, -1, 0) * moveSpeed;
            }

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector3(-1, 1, 0) * moveSpeed;
            }

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector3(-1, -1, 0) * moveSpeed;
            }

            if (Input.GetKeyUp(KeyCode.S) && Input.GetKeyUp(KeyCode.W))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            }

            if (Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.D))
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }

            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
    }

    public void StopKnockedInAir()
    {
        anim.SetBool("KnockedInTheAir", false);
        knockedInTheAir = false;
        canMove = true;
    }
}
