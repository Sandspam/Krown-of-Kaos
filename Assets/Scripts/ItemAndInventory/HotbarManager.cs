using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    public GameObject[] hotbars;

    private int itemIDAdding;
    private AudioSource aus;
    public AudioClip gulpSound;

    private void Update()
    {
        aus = gameObject.GetComponent<AudioSource>();
    }

    public void AddItem(int instanceItemID)
    {
        itemIDAdding = instanceItemID;
        for (int i = 0; i < hotbars.Length; i++)
        {
            if(hotbars[i].GetComponent<Hotbar>().itemID == 0)
            {
                //Add item ID
                hotbars[i].GetComponent<Hotbar>().itemID = instanceItemID;
                hotbars[i].transform.GetChild(0).GetComponent<Image>().color = new Vector4(255, 255, 255, 255);
                break;
            }
        }
    }

    public void RemoveItem(int instanceItemID)
    {
        Debug.Log("Finding item...");
        for (int i = 0; i < hotbars.Length; i++)
        {
            if(hotbars[i].GetComponent<Hotbar>().itemID == instanceItemID)
            {
                hotbars[i].GetComponent<Hotbar>().itemID = 0;
                hotbars[i].transform.GetChild(0).GetComponent<Image>().color = new Vector4(255, 255, 255, 0);
            }
        }
    }

    public void UseItem(int hotbarID)
    {
        string itemType = gameObject.transform.GetChild(hotbarID).GetComponent<Hotbar>().itemType;
        if (itemType == "Consumable")
        {
            aus.clip = gulpSound;
            aus.Play();
            string potionType = gameObject.transform.GetChild(hotbarID).GetComponent<Hotbar>().potionType;
            if (potionType == "HealthRegen")
            {
                int instanceItemID = gameObject.transform.GetChild(hotbarID).GetComponent<Hotbar>().itemID;
                //Restores the health based on the amount of health the item in the Item Dictionary is set to.
                GameObject.Find("Player").GetComponent<PlayerHealthManager>().RestoreHealth(GameObject.Find("ItemDictionary").GetComponent<ItemDictionary>().items[instanceItemID].restoreAmount);
                gameObject.transform.GetChild(hotbarID).GetComponent<Hotbar>().itemID = 0;
                gameObject.transform.GetChild(hotbarID).GetComponent<Hotbar>().itemImageHolder.color = new Vector4(255, 255, 255, 0);
            }

            if(potionType == "Clear")
            {
                int instaceItemID = gameObject.transform.GetChild(hotbarID).GetComponent<Hotbar>().itemID;
                var projectiles = FindObjectsOfType<Fireball>();
                for (int i = 0; i < projectiles.Length; i++)
                {
                    Destroy(projectiles[i].gameObject);
                }
                gameObject.transform.GetChild(hotbarID).GetComponent<Hotbar>().itemID = 0;
                gameObject.transform.GetChild(hotbarID).GetComponent<Hotbar>().itemImageHolder.color = new Vector4(255, 255, 255, 0);
            }
        }
    }
}
