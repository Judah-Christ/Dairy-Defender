using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Item", menuName = "Shop/Item", order = 1)]
public class MyScriptableObjectClass : ScriptableObject
{
    public string itemName;
    public GameObject itemObject;
    public int itemCost;
    public ItemType itemType;
    public int Health;

}

public enum ItemType
{
    TOWER,
    AMMO,
}