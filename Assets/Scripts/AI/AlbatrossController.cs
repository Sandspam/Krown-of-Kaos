using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbatrossController : MonoBehaviour
{
    public enum Phase
    {
        FREEROAM,
        ATTACK
    }

    //General Variables
    public Phase phase;
    public bool canMove;
    private Animator anim;
    private Rigidbody2D myRigidbody;
    private GameObject player;
    [HideInInspector] public bool playerInRange;

    //Free Roam Variables
    public float freeRoamSpeed;
    public float timeBetweenMove;
    public float timeToMove;
    private float timeBetweenMoveCounter;
    private float timeToMoveCounter;
    private bool canFreeRoam;
    private Vector3 moveDirection;

    //Atac Variables
    public GameObject target;
    public float combatMoveSpeed;
    public float timeBetweenSwing;
    private float swingTimer;

    private void Start()
    {
        swingTimer = timeBetweenSwing;
        anim = GetComponent<Animator>();
        canFreeRoam = true;
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        target = player;

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
    }

    private void Update()
    {
        switch(phase)
        {
            case Phase.FREEROAM:
                HandleFreeRoam();
                break;

            case Phase.ATTACK:
                HandleAttack();
                break;
        }

        if(playerInRange)
        {
            phase = Phase.ATTACK;
        }
    }

    void HandleFreeRoam()
    {
        //If the enemy can free roam...
        if (canFreeRoam)
        {
            //Start the counter...
            timeToMoveCounter -= Time.deltaTime;
            //Apply movement to the rigidbody [X]
            myRigidbody.velocity = moveDirection;

            //If the time to move has been spent then...
            if (timeToMoveCounter <= 0f)
            {
                //Enemy can't free roam anymore 
                canFreeRoam = false;

                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
            }
        }

        if (!canFreeRoam)
        {
            myRigidbody.velocity = Vector3.zero;
            timeBetweenMoveCounter -= Time.deltaTime;
            if (timeBetweenMoveCounter <= 0f)
            {
                //Enemy can roam
                canFreeRoam = true;
                //Reset the time between move counter
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);

                //Apply movement to move direction [X]
                moveDirection = new Vector3(Random.Range(-1f, 1f) * freeRoamSpeed, Random.Range(-1f, 1f) * freeRoamSpeed, 0);
            }
        }
    }

    void HandleAttack()
    {
        if (target != null)
        {
            swingTimer -= Time.deltaTime;
            if (swingTimer <= 0)
            {
                SwordSwing();
                swingTimer = timeBetweenSwing;
            }
            transform.rotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
            if (canMove)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, combatMoveSpeed);
            }
        }
    }

    public void SwordSwing()
    {
        anim.SetBool("Swinging", true);
    }

    public void StopSwing()
    {
        anim.SetBool("Swinging", false);
    }
}