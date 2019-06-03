using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public int itemID;
    public string itemType;
    public string potionType;
    public int restoreAmount;
    public Sprite itemImage;
    public Color itemColor = new Vector4(255, 255, 255, 255);
}
