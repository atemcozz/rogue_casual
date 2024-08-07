using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] List<InventoryCell> cells;
    [SerializeField] InventoryCell sampleCell;
    [SerializeField] GameObject interactButton;
    [SerializeField] PlayerControls player;
    // Start is called before the first frame update
    void OnEnable(){
        inventory.OnItemsChanged+=UpdateInventory;
    }
    void OnDisable(){
        inventory.OnItemsChanged-=UpdateInventory;
    }
    void Start()
    {
        
    }
    void Update(){
        if(player.CollidedDrop != null){
            interactButton.SetActive(true);
        }
        else interactButton.SetActive(false);
    }

    // Update is called once per frame
    void UpdateInventory(List<ItemStat> items)
    {
        foreach(Transform cell in transform){
            Destroy(cell.gameObject);
        }
        cells.Clear();
        for(int i = 0;i<items.Count;i++){
          InventoryCell cell = Instantiate(sampleCell,Vector3.zero,Quaternion.identity,transform);
            cell.itemIcon.sprite = items[i].icon;
            cell.index = i;
          cell.SetQuantity(items[i].Quantity);
            cell.OnDelete += DeleteItem;
            cell.OnPress += UpdateEquippedWeapon;
            cells.Add(cell);
        }

    }
    void DeleteItem(int index){
        inventory.RemoveItemByIndex(index);
    }
    void UpdateEquippedWeapon(int index){
        inventory.UpdateEquippedWeapon(index);
    }
}
