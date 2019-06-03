using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            gameObject.transform.parent.gameObject.GetComponent<AlbatrossController>().playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            gameObject.transform.parent.gameObject.GetComponent<AlbatrossController>().playerInRange = false;
        }
    }
}
