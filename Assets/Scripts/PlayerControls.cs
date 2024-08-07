using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControls : MonoBehaviour
{
    PlayerInput input;
    [SerializeField] float speed;
    [SerializeField] SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    [SerializeField] Weapon weapon;
    [SerializeField] Transform weaponHolder;
    [SerializeField] ItemDrop itemDropBlank;
    [SerializeField] Inventory inventory;
    [SerializeField] ItemStat baseWeaponStat;
    [SerializeField]float itemPickRadius =1f;
    [SerializeField] LayerMask dropLayers;
    ItemDrop collidedDrop;
    public ItemDrop CollidedDrop => collidedDrop;
    ItemStat equippedWeapon;
    
    // Start is called before the first frame update
    void Start()
    {
        input = PlayerInput.Instance;
        rb = GetComponent<Rigidbody2D>();
        ItemStat baseWeapon = ScriptableObject.Instantiate(baseWeaponStat);
        inventory.AddItem(baseWeapon);
      //  EquipWeapon(baseWeapon);


    }
    void Flip(bool  value){
        spriteRenderer.flipX = value;
      if(weapon != null)  weapon.Flip(value);
    }
    void OnLook(){
        if(input.layout == PlayerInput.ControlLayout.PC){
          Flip(((Vector2)transform.position - input.LookPointer).x > 0);
          if(weapon.Stat.Rotatable && weapon != null) weapon.Rotate((input.LookPointer - (Vector2)transform.position).normalized);
         
        }
        else if(input.layout == PlayerInput.ControlLayout.Mobile && input.LookPointer.x != 0){
            if(weapon != null) {
                weapon.Attack();
             }
           Flip(input.LookPointer.x < 0);
            if(weapon.Stat.Rotatable && weapon != null) weapon.Rotate(input.LookPointer);
        }
    }
    public void PickDrop(){
        

        
        if(collidedDrop != null){
                inventory.AddItem(collidedDrop.Item);
                Destroy(collidedDrop.gameObject);
            }
    }
    void CheckForItems(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,itemPickRadius,dropLayers);
        if(colliders != null && colliders.Length > 0) collidedDrop = colliders[0].GetComponent<ItemDrop>();
        else collidedDrop = null;
    }
    void Update(){ 
        if(Input.GetKeyDown(KeyCode.F)){
             PickDrop();
            }
            if(Input.GetKeyDown(KeyCode.G)){
              var item = ScriptableObject.Instantiate(weapon.Stat);
              item.Ammo = 5;
             // print(item.Ammo);
            }
        if(weapon == null){
            //weapon = Instantiate(Resources.Load<Weapon>("Weapons/IronSword"),transform.position,Quaternion.identity,weaponHolder);
           
        }
          
          if(Input.GetMouseButton(0)){
             if(weapon != null && input.layout == PlayerInput.ControlLayout.PC) {
                weapon.Attack();
                // print(weapon.Stat.Ammo);
             }
            
          }
            CheckForItems();
           OnLook();
           
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        ProcessMovement();
    }
    public void DropItem(ItemStat item){

       ItemDrop drop = Instantiate(itemDropBlank,transform.position,Quaternion.identity);
       drop.SetDrop(item);
       /*
      if(weapon != null && item.itemType == ItemStat.ItemType.Weapon) {
          Destroy(weapon.gameObject);
          equippedWeapon = null;
      }*/
       
    }
    public void DestroyCurrentWeapon(){
        if(weapon != null) {
         //   print(weapon.Stat.Ammo);
          Destroy(weapon.gameObject);
          
      }
    }
    public void EquipWeapon(ItemStat item){
      if(weapon != null) Destroy(weapon.gameObject);
         weapon = Instantiate(item.itemPrefab,transform.position,Quaternion.identity,weaponHolder).GetComponent<Weapon>();
         weapon.Stat = (WeaponStat)item;
    }
    void ProcessMovement(){
        Vector2 direction = input.Movement;
        rb.velocity = direction * speed;
    }
    void OnTriggerEnter2D(Collider2D trigger){
        if(trigger.TryGetComponent(out ItemDrop drop)){
           // collidedDrop = drop;
        }
        
    }
    void OnTriggerExit2D(Collider2D trigger){
        if(trigger.TryGetComponent(out ItemDrop drop)){
           // collidedDrop = null;
        }
    }
    public ItemStat GetEquippedWeapon(){
        return equippedWeapon;
    }
}
