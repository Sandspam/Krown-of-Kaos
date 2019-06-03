using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public float invincibilityTimeAfterHit;
    private float invincibilityTimer;
    private bool hasInvincibility;
    public bool dropsItem;
    public int itemIDToDrop;
    public GameObject itemPickUpPrefab;
    private Animator anim;
    private bool isBeingHurt;
    public float hurtAnimationTime;
    private float hurtAnimationTimer;
    public bool isBoss;
    private GameObject player;
    private Transform playerReturn;


    private void Start()
    {
        player = GameObject.Find("Player");
        playerReturn = GameObject.Find("PlayerReturnPoint").transform;
        hurtAnimationTimer = hurtAnimationTime;
        anim = gameObject.transform.parent.GetComponent<Animator>();
        invincibilityTimer = invincibilityTimeAfterHit;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (hasInvincibility)
        {
            invincibilityTimer -= Time.deltaTime;
        }

        if (isBeingHurt)
        {
            //Change color in the animation "Damaged"
            anim.SetBool("Damaged", true);
            hurtAnimationTimer -= Time.deltaTime;
        }

        if (hurtAnimationTimer <= 0)
        {
            anim.SetBool("Damaged", false);
            isBeingHurt = false;
            hurtAnimationTimer = hurtAnimationTime;
        }

        if (invincibilityTimer <= 0f)
        {
            hasInvincibility = false;
            invincibilityTimer = invincibilityTimeAfterHit;
        }

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("VictoryScreen");
        }
    }

    public void TakeDamage(int damage, Collider2D targetCollider)
    {
        if (!hasInvincibility)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Time.timeScale = 0f;
                SceneManager.LoadScene("VictoryScreen");
            }

            else
            {
                player.GetComponent<PlayerMovement>().knockedInTheAir = true;
                player.GetComponent<Animator>().SetBool("KnockedInTheAir", true);
                player.GetComponent<PlayerMovement>().canMove = false;
                var projectiles = FindObjectsOfType<Fireball>();
                for (int i = 0; i < projectiles.Length; i++)
                {
                    Destroy(projectiles[i].gameObject);
                }
                //player.transform.position = playerReturn.transform.position;
                isBeingHurt = true;
                hasInvincibility = true;
                Vector3 force = transform.position - targetCollider.transform.position;
                force.Normalize();
                //transform.GetComponent<Rigidbody2D>().AddForce(-force * pushbackForce);
            }
        }
    }
}
