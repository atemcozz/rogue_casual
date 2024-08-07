using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;
    public ControlLayout layout;
    [SerializeField] Joystick movementJoystick;
     [SerializeField] Joystick lookJoystick;
     public UnityAction OnLookJoystickDrag;
     [SerializeField]  PlayerControls player;
     Vector2 movement;
     Vector2 look;

     void OnEnable(){
        lookJoystick.OnJoystickDrag += () => OnLookJoystickDrag?.Invoke();
     }
     void OnDisable(){

     }
     Camera cachedCamera;
     
    private void Awake() {
        if(Instance == null){
            Instance = this;
        }
      
    }
    private void Start() {
        
        cachedCamera = Camera.main;
       /*
        #if UNITY_STANDALONE
            layout = ControlLayout.PC;
        #endif
        #if UNITY_ANDROID
            layout = ControlLayout.Mobile;
        #endif
        #if UNITY_WEBGL
            layout = ControlLayout.Mobile;
        #endif
         #if UNITY_EDITOR
            layout = ControlLayout.PC;
        #endif */
    }
public enum ControlLayout{
    PC,
    Mobile,
}
public Vector2 LookPointer{
    get{
        if(layout == ControlLayout.PC){
            return cachedCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        if(layout == ControlLayout.Mobile){
           return lookJoystick.Direction;
        }
        else return Vector2.zero;
    }
}
    public Vector2 Movement{
        get{
            return movement;
        }
    }
     public Vector2 Look{
        get{
            return look;
        }
    }
   
    void ProccessInput(){
        if(layout == ControlLayout.PC){
           movement = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        }
        if(layout == ControlLayout.Mobile){
           movement = new Vector2(movementJoystick.Horizontal,movementJoystick.Vertical);
        }
        
    }
  

    // Update is called once per frame
    void Update()
    {
        ProccessInput();

    }
    
}
