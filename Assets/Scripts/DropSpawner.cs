using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 #if UNITY_EDITOR
[ExecuteInEditMode] 
#endif
public class DropSpawner : MonoBehaviour
{
     SpriteRenderer spriteRenderer;
     [SerializeField] ItemStat item;

     void OnEnable(){
        
         
         
     }
     void Awake(){
           spriteRenderer = GetComponent<SpriteRenderer>();  
         
         
     }
     void Start(){
         // item = ScriptableObject.Instantiate(item); 
         // spriteRenderer.sprite = item.icon;
         if(Application.isPlaying){
             ItemDrop drop = Instantiate(Resources.Load<ItemDrop>("ItemDrop"),transform.position,Quaternion.identity);
         drop.SetDrop(ScriptableObject.Instantiate(item));
         Destroy(gameObject);
         }
         
     }
     #if UNITY_EDITOR
     
       void Update(){
            spriteRenderer.sprite = item.icon;
        }
     #endif
}
