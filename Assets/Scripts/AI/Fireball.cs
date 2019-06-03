using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int fireballDamage;
    private float collTimer = 2f;
    public CapsuleCollider2D caps;

    private void Start()
    {
        caps = gameObject.GetComponent<CapsuleCollider2D>();
        caps.enabled = false;
    }

    private void Update()
    {
        collTimer -= 0.1f;
        if(collTimer <= 0)
        {
            caps.enabled = true;
        }
        transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 90);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerHealthManager>().TakeDamage(fireballDamage, collision);
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Nonwalkables")
        {
            Destroy(gameObject);
        }
    }
}
