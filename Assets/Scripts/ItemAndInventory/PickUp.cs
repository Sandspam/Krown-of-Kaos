using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public int itemID;
    private SpriteRenderer itemSprite;
    private ItemDictionary itD;
    private HotbarManager hbM;
    private AudioManager cameraObject;

    private void Start()
    {
        cameraObject = GameObject.Find("Main Camera").GetComponent<AudioManager>();
        hbM = FindObjectOfType<HotbarManager>();
        itD = FindObjectOfType<ItemDictionary>();
        itemSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        itemSprite.sprite = itD.items[itemID].itemImage;
        itemSprite.color = itD.items[itemID].itemColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            cameraObject.PlayPickUpSound();
            hbM.AddItem(itemID);
            Destroy(gameObject);
        }
    }
}
