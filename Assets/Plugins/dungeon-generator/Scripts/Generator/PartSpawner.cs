using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSpawner : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] LocationPart parent;
    bool isGenerated = false;
    public bool IsGenerated => isGenerated;
    void Start()
    {
     //  Spawn();
    
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.DrawRay(transform.position,transform.up,Color.red);
        if(Input.GetKeyDown(KeyCode.F)){
            Spawn();

        }
        if(isGenerated && transform.childCount == 0){
            wall.SetActive(true);
        }
    }
   public void Spawn(){
        GameObject[] parts = GenerationManager.Instance.LocationParts;
        GameObject selectedPart = parts[Random.Range(0,parts.Length)];
        /*
        if(!Physics.Raycast(transform.position,transform.forward,3f)){
             LocationPart newPart = Instantiate(selectedPart,transform.position,transform.rotation).GetComponent<LocationPart>();
             
            
        }*/
         if(!Physics2D.Raycast(transform.position,transform.up,Mathf.Infinity,GenerationManager.Instance.RoomLayers) && GenerationManager.Instance.CurrentRoomID < GenerationManager.Instance.MinRoomCount){
             LocationPart newPart = Instantiate(selectedPart,transform.position,transform.rotation,transform).GetComponent<LocationPart>();
             newPart.id = GenerationManager.Instance.CurrentRoomID;
             GenerationManager.Instance.UpdateRoomID();
             newPart.ancestor = parent;
             
            
        }
        else{
             wall.SetActive(true);
             
        }
        isGenerated = true;
        //Destroy(gameObject);
        /*
        for(int i = 0;i< parts.Length;i++){
            LocationPart newPart = Instantiate(parts[i],transform.position,transform.rotation).GetComponent<LocationPart>();
            if(!newPart.IsValid){
                Destroy(newPart.gameObject);
            }
            else {
                break;
            }
        } */

       
       // Destroy(gameObject);
    }
}
