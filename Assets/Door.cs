using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int requestedItemID;
    private HotbarManager hbM;


    void Start()
    {
        hbM = FindObjectOfType<HotbarManager>().GetComponent<HotbarManager>();
    }

    void OnCollisionEnter2D (Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            for (int i = 0; i < hbM.hotbars.Length; i++)
            {
                if (hbM.hotbars[i].GetComponent<Hotbar>().itemID == requestedItemID)
                {
                    hbM.RemoveItem(requestedItemID);
                    Destroy(gameObject);
                }
            }
        }
    }
}
