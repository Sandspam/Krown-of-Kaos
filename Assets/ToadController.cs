using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadController : MonoBehaviour
{

    public enum Phase
    {
        FREEROAM,
        IDLE
    }

    //General Variables
    public Phase currentPhase;
    private Rigidbody2D rb2D;
    private Animator anim;

    //Free Roam Variables
    public float freeRoamSpeed;
    public float timeBetweenMove;
    public float timeToMove;
    private float timeBetweenMoveCounter;
    private float timeToMoveCounter;
    private bool canFreeRoam;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentPhase)
        {
            case Phase.FREEROAM:
                HandleFreeroam();
                break;

            case Phase.IDLE:
                HandleIdle();
                break;
        }
    }

    void HandleFreeroam ()
    {
        anim.SetBool("Jumping", true);
        //Start the counter...
        timeToMoveCounter -= Time.deltaTime;
        //Apply movement to the rigidbody [X]
        rb2D.velocity = moveDirection;

        //If the time to move has been spent then...
        if (timeToMoveCounter <= 0f)
        {
            timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
            currentPhase = Phase.IDLE;
        }
    }

    void HandleIdle ()
    {
        anim.SetBool("Jumping", false);
        rb2D.velocity = Vector3.zero;
        timeBetweenMoveCounter -= Time.deltaTime;
        if (timeBetweenMoveCounter <= 0f)
        {
            //Reset the time between move counter
            timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            moveDirection = new Vector3(Random.Range(-1f, 1f) * freeRoamSpeed, Random.Range(-1f, 1f) * freeRoamSpeed, 0);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
            currentPhase = Phase.FREEROAM;
        }
    }
}
