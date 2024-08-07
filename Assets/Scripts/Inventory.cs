using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] float inventorySize;
    [SerializeField] PlayerControls player;
    public float Size => inventorySize;
    public float CurrentSize => items.Count;
   List<ItemStat> items = new List<ItemStat>();
   public UnityAction<List<ItemStat>> OnItemsChanged;
   ItemStat equippedWeapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddItem(ItemStat item){
        for(int i = 0;i<items.Count;i++){
            if(item.key == items[i].key && item.Stackable){
                print(1);
                items[i].Quantity += item.Quantity;
                 OnItemsChanged?.Invoke(items);
                 return;
            }
        }
        items.Add(item);
        OnItemsChanged?.Invoke(items);
        if(item.itemType == ItemStat.ItemType.Weapon){ 
            if(GetWeapons()?.Count <= 1) {
                player.EquipWeapon(item);
                equippedWeapon = item;
                }
        }
       // if(item.)
    }
    public void RemoveItem(ItemStat item){
        items.Remove(item);
        OnItemsChanged?.Invoke(items);
        
    }
    public void RemoveItemByIndex(int index){
        ItemStat item = items[index];
        items.RemoveAt(index);  
        player.DropItem(item);
        if(item == equippedWeapon){
            player.DestroyCurrentWeapon();
            if(GetWeapons()?.Count >= 1){
                player.EquipWeapon(GetWeapons()[0]);
            equippedWeapon = GetWeapons()[0];
            }
            
        }
         OnItemsChanged?.Invoke(items);
    }
    public void UpdateEquippedWeapon(int index){
        ItemStat item = items[index];
        if(item.itemType == ItemStat.ItemType.Weapon){
             player.EquipWeapon(item);
            equippedWeapon = item;
        }
       
    }
    public List<ItemStat> GetWeapons(){
         List<ItemStat> weapons = new List<ItemStat>();
         if(items.Count > 0){
             for(int i = 0; i<items.Count; i++){
                if(items[i].itemType == ItemStat.ItemType.Weapon) weapons.Add(items[i]);
            }
            return weapons;
         }
         else return null;
         
    }
}
