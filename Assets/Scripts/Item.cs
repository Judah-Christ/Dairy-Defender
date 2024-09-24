using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Shop/Item", order = 1)]
public class GameItem : ScriptableObject
{
    public string itemName;
    public GameObject itemObject;
    public Sprite itemSprite;
    public Image itemImage;
    public int itemCost;
    public ItemType itemType;
    public int Health;

}

public enum ItemType
{
    TOWER,
    AMMO,
}