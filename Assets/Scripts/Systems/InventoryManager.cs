using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager inventory;

    [SerializeField] int baseItemCount;
    [SerializeField] int keyItemCount;

    void Start()
    {
        inventory = this;
    }

    public void BaseItemCollected(int value)
    {
        baseItemCount += value;
    }
    
    public void KeyItemCollected()
    {
        keyItemCount ++;
    }
}
