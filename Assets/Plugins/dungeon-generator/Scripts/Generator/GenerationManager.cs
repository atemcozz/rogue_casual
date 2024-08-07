using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GenerationManager : MonoBehaviour
{
    public bool OcclusionCulling = true;
    [SerializeField] RoomSet roomSet;
   [SerializeField] [Min(0)]  int minRoomCount = 10;
    public int MinRoomCount => minRoomCount;
   [Min(0)] [SerializeField] int currentRoomID = 0;
     public int CurrentRoomID => currentRoomID;
     [SerializeField] LayerMask roomLayers;
     public LayerMask RoomLayers => roomLayers;

     static GenerationManager _instance;
     public static GenerationManager Instance => _instance;
   
    GameObject[] locationParts;

    public GameObject[] LocationParts => locationParts;
     NavMeshSurface surface;
     bool generationEnd = false;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Awake()
    {
        if(_instance == null){
          _instance = this;
        }
         locationParts = roomSet.Rooms;
       
    }
    void Start(){
        GenerateDungeon();
        surface = GetComponent<NavMeshSurface>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
       if(!generationEnd) CheckForGenerationEnd();
       
    }
    public void GenerateDungeon(){
        currentRoomID =0;
        foreach(Transform child in transform){
            Destroy(child.gameObject); 
        }
       LocationPart part = Instantiate(locationParts[0],Vector3.zero,Quaternion.identity,transform).GetComponent<LocationPart>();
       part.rootRoom = true;
    }
    public void UpdateRoomID(){
        currentRoomID++;
    }
    public void RemoveRoom(){
        currentRoomID--;
    }
    void CheckForGenerationEnd(){

            
           // yield return new WaitForSeconds(0.1f);
           PartSpawner[] allParts = FindObjectsOfType<PartSpawner>();
           foreach(PartSpawner part in allParts){
               if(!part.IsGenerated && part.gameObject.activeInHierarchy){
                   return;
               }
           }
        if(currentRoomID<minRoomCount){
             GenerateDungeon();
        }
        else{
            generationEnd = true;
            //surface.BuildNavMesh();
        } 
        
       
    }
    
}
