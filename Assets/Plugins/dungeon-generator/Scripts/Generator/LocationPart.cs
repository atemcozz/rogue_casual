using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationPart : MonoBehaviour
{
    bool isValid = true;
    public bool IsValid => isValid;
    bool check = false;
    public int id;
    public LocationPart ancestor;
        BoxCollider2D _collider;
        public bool rootRoom;
   Camera _camera;

    void Awake()
    {
      _collider = GetComponent<BoxCollider2D>();
    }
    void Start(){
     _camera = Camera.main;
    }
    void Update()
    {
        /*
        if(ancestor == null && !rootRoom){
             Destroy(gameObject);
         }*/
     if(Input.GetKeyDown(KeyCode.A)){
         if(ancestor == null && !rootRoom){
             Destroy(gameObject);
         }
     }
     if(GenerationManager.Instance.OcclusionCulling){
         if(!IsTargetVisible(_camera,transform.GetChild(0).gameObject)){
         transform.GetChild(0).gameObject.SetActive(false);
     }
     else{
          transform.GetChild(0).gameObject.SetActive(true);
     }
     }
     
    }
    void OnTriggerStay2D(Collider2D trigger){
        
       
             if(trigger.TryGetComponent(out LocationPart room) && room.id<this.id){
           isValid = false;
           GenerationManager.Instance.RemoveRoom();
          // print("remove");
           Destroy(gameObject);

            }
       
        
       
    }
    public bool CheckValidation(){
        return isValid;
    }
    bool IsTargetVisible(Camera c,GameObject go)
       {
          var planes = GeometryUtility.CalculateFrustumPlanes(c);
          var point = go.transform.position;
          foreach (var plane in planes)
          {
               if (plane.GetDistanceToPoint(point) < 0)
                   return false;
           }
           return true;
       }
   
}
