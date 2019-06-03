using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public enum Phase
    {
        IDLE,
        SHOTGUN,
        FIREBALL
    }

    //General Variables
    public Phase currentBossPhase;
    private Animator anim;
    private GameObject player;
    public GameObject fireballPrefab;
    private GameObject Looktarget;

    /*Moving Variables
    public float speed;
    public Transform[] moveSpots;
    public float rotateSpeed;
    public float rotateTimer;
    private Quaternion qTo;
    private int randomSpot;*/

    //Shotgun Variables
    List<Quaternion> fireballs;
    public float spreadAngle;
    public int fireballCount;
    public float timeBetweenShotgun;
    private float shotgunTimer;

    //Fireball Variables
    public float fireballSpeed;
    public float timeBetweenAttack = 3f;
    private float attackTimer;

    // Start is called before the first frame update
    void Start()
    {
        shotgunTimer = timeBetweenShotgun;
        fireballs = new List<Quaternion>(fireballCount);
        for (int i = 0; i < fireballCount; i++)
        {
            fireballs.Add(Quaternion.Euler(Vector3.zero));
        }
        Looktarget = GameObject.Find("PlayerReturnPoint");
        anim = gameObject.GetComponent<Animator>();
        //qTo = Quaternion.Euler(new Vector3(0.0f, Random.Range(-180.0f, 180.0f), 0.0f));
        attackTimer = timeBetweenAttack;
        player = GameObject.Find("Player");
        //randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentBossPhase)
        {
            case Phase.FIREBALL:
                HandleFireball();
                break;

            case Phase.SHOTGUN:
                HandleShotgun();
                break;

            case Phase.IDLE:
                HandleIdle();
                break;
        }
    }

    void HandleIdle()
    {

    }

    void HandleShotgun ()
    {
        shotgunTimer -= Time.deltaTime;
        if (shotgunTimer <= 0)
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, target.transform.position + transform.position);
            /*int i = 0;
            foreach (Quaternion quat in fireballs)
            {
                fireballs[i] = Random.rotation;
                GameObject p = Instantiate(fireballPrefab, gameObject.transform.position, Quaternion.identity);
                p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, fireballs[i], spreadAngle);
                p.GetComponent<Rigidbody2D>().AddForce(p.transform.right * -fireballSpeed);
                i++;
            }*/
            for (int i = 0; i < fireballCount; i++)
            {
                GameObject p = Instantiate(fireballPrefab, gameObject.transform.position, Quaternion.identity);
                p.GetComponent<Fireball>().caps = p.GetComponent<CapsuleCollider2D>();
                p.GetComponent<Fireball>().caps.enabled = false;
                Quaternion curRotation = Random.rotation;
                //curRotation.eulerAngles = new Vector3(0, 0, curRotation.z);
                p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, curRotation, spreadAngle);
                //p.transform.eulerAngles = new Vector3(0, 0, p.transform.rotation.z);
                p.GetComponent<Rigidbody2D>().AddForce(p.transform.right * -fireballSpeed);
                shotgunTimer = timeBetweenShotgun;
            }
        }
    }

    void HandleFireball()
    {
        anim.SetBool("Casting", true);
        Vector2 target = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = target - myPos;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 180f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        GameObject projectile = (GameObject)Instantiate(fireballPrefab, gameObject.transform.GetChild(1).transform.position, rotation);
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        projectile.GetComponent<Rigidbody2D>().velocity = (direction * fireballSpeed);

        //currentBossPhase = Phase.MOVE;
    }

    /*void HandleMovement()
    {
        attackTimer -= Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if (transform.position == moveSpots[randomSpot].position)
        {
            randomSpot = Random.Range(0, moveSpots.Length);
        }

        if (attackTimer <= 0)
        {
            attackTimer = timeBetweenAttack;
            currentBossPhase = Phase.FIREBALL;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, Time.deltaTime * rotateSpeed);
    }*/

    public void SetCastFalse()
    {
        anim.SetBool("Casting", false);
    }

    public void SetHurtFalse ()
    {
        anim.SetBool("Damaged", false);
    }
}
