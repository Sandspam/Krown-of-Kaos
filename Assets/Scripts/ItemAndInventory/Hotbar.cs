using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public int hotbarID;
    public int itemID;
    public string itemType;
    public string potionType;
    public Color itemColor;

    public ItemDictionary itemD;
    public Image itemImageHolder;
    public Text itemNameHolder;

    private void Start()
    {
        itemD = FindObjectOfType<ItemDictionary>().GetComponent<ItemDictionary>();
        itemImageHolder = gameObject.transform.GetChild(0).GetComponent<Image>();
        itemNameHolder = gameObject.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        potionType = itemD.items[itemID].potionType;
        itemType = itemD.items[itemID].itemType;
        itemImageHolder.sprite = itemD.items[itemID].itemImage;
        itemNameHolder.text = itemD.items[itemID].itemName;
        itemColor = itemD.items[itemID].itemColor;
        itemImageHolder.color = itemColor;
    }
}
