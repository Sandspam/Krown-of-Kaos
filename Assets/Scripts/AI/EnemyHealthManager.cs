    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
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
    public AudioClip hurtAudioClip;
    public AudioClip deathAudioClip;
    private AudioSource aus;


    private void Start()
    {
        aus = gameObject.GetComponent<AudioSource>();
        aus.clip = hurtAudioClip;
        hurtAnimationTimer = hurtAnimationTime;
        anim = gameObject.GetComponent<Animator>();
        invincibilityTimer = invincibilityTimeAfterHit;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (hasInvincibility)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        
        if(isBeingHurt)
        {
            anim.SetBool("IsHurt", true);
            hurtAnimationTimer -= Time.deltaTime;
        }

        if(hurtAnimationTimer <= 0)
        {
            anim.SetBool("IsHurt", false);
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
            aus.clip = deathAudioClip;
            aus.Play();
            if(dropsItem)
            {
                GameObject itemInstance = Instantiate(itemPickUpPrefab, transform.position, Quaternion.identity);
                itemInstance.GetComponent<PickUp>().itemID = itemIDToDrop;
            }

            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage, Collider2D targetCollider)
    {
        if (!hasInvincibility)
        {
            aus.clip = hurtAudioClip;
            aus.Play();
            isBeingHurt = true;
            currentHealth -= damage;
            hasInvincibility = true;
            Vector3 force = transform.position - targetCollider.transform.position;
            force.Normalize();
            //transform.GetComponent<Rigidbody2D>().AddForce(-force * pushbackForce);
        }
    }
}
