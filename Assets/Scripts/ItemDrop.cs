using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
     [SerializeField] ItemStat item;
     public ItemStat Item => item;
     void OnEnable(){
         spriteRenderer = GetComponent<SpriteRenderer>(); 
     }
     void Start(){
          
         
     }
   
     
    public void SetDrop(ItemStat item){
        this.item = item;
        spriteRenderer.sprite = item.icon;
    }
}
