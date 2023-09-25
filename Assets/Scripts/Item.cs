using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Common = 0,
    Uncommon = 10,
    Rare = 20,
    Legendary = 30
}

public class Item : MonoBehaviour
{
    public int value = 1;
    public string itemName = "Item";
    public Rarity rarity;
}
