using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    private GameObject player;
    public bool isAlbatross;
    public bool isPlayer;
    public int damage;
    public float pushbackForce;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAlbatross)
        {
            if (collision.gameObject.tag == "Player")
            {
                player.GetComponent<PlayerHealthManager>().TakeDamage(damage, collision);
            }
        }

        if (isPlayer)
        {
            if(collision.gameObject.tag == "Enemy")
                collision.transform.parent.GetComponent<EnemyHealthManager>().TakeDamage(damage, collision);

            if(collision.gameObject.tag == "Boss")
                collision.transform.GetComponent<BossHealthManager>().TakeDamage(damage, collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isAlbatross)
        {
            if (collision.gameObject.tag == "Player")
            {
                player.GetComponent<PlayerHealthManager>().TakeDamage(damage, collision);
            }
        }

        if (isPlayer)
        {
            if (collision.gameObject.tag == "Enemy")
                collision.transform.parent.GetComponent<EnemyHealthManager>().TakeDamage(damage, collision);

            if (collision.gameObject.tag == "Boss")
                collision.transform.GetComponent<BossHealthManager>().TakeDamage(damage, collision);
        }
    }
}
