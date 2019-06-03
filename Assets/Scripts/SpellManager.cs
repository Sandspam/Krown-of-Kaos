using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public GameObject player;
    public int projectileSpeed;
    public GameObject fireBall;
    public GameObject spellSpawnPoint;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Fireball()
    {
        GameObject instance = Instantiate(fireBall, spellSpawnPoint.transform.position, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().AddForce((player.GetComponent<PlayerMovement>().mousePos - transform.position) * projectileSpeed);
    }
}
