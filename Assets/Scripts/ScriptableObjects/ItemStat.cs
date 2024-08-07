using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class ItemStat : ScriptableObject
{  
    public enum ItemType{Weapon,Armor,Item} 
    public ItemType itemType;
    public Sprite icon;
    public string itemName; 
    public GameObject itemPrefab;
    [TextArea]public string description;
    [Min(0)]public int rarity;
    [Min(0)]public int cost;
    [Min(0)]public int weight;
    public bool Stackable = false;
   [HideInInspector] [Min(1)]public int Quantity = 1;
    public string key;
    
}
    

