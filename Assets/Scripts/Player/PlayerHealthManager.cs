using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float invincibilityTimeAfterHit;
    private float invincibilityTimer;
    private bool hasInvincibility;
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        invincibilityTimer = invincibilityTimeAfterHit;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(hasInvincibility)
        {
            anim.SetBool("IsHurt", true);
            invincibilityTimer -= Time.deltaTime;
        }
        else
        {
            anim.SetBool("IsHurt", false);
        }

        if (invincibilityTimer <= 0f)
        {
            hasInvincibility = false;
            invincibilityTimer = invincibilityTimeAfterHit;
        }

        if(currentHealth <= 0)
        {
            SceneManager.LoadScene("DefeatScene");
        }

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(int damage, Collider2D targetCollision)
    {
        if(!hasInvincibility)
        {
            currentHealth -= damage;
            hasInvincibility = true;
            Vector3 force = transform.position - targetCollision.transform.position;
            force.Normalize();
            //transform.GetComponent<Rigidbody2D>().AddForce(-force * pushbackForce);
        }
    }

    public void RestoreHealth(int healthToRestore)
    {
        currentHealth += healthToRestore;
    }
}
