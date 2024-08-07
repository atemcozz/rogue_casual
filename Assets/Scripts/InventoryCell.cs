using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class InventoryCell : MonoBehaviour
{
    public Image itemIcon;
    public int index;
    public UnityAction<int> OnDelete, OnPress;
    [SerializeField] Text quantityText;
     [SerializeField]GameObject quantityBlank;
    public void OnDeleteButtonDown(){
        OnDelete?.Invoke(index);
    }
    public void OnButtonPress(){
        OnPress?.Invoke(index);
        
    }
    public void SetQuantity(int quantity){
        if(quantity>1) quantityText.text = quantity.ToString();
        else quantityBlank.SetActive(false);
    }
    void Update(){
        if(Input.GetKeyDown((index+1).ToString())){
             OnPress?.Invoke(index);
        }
    }
}
