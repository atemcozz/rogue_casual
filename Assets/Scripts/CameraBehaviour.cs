using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followingSpeeed;
    Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update(){}
    // Update is called once per frame
    void FixedUpdate()
    {
        Follow(target.position);
    }
    void Follow(Vector3 target){
        Vector3 corrected = new Vector3(target.x,target.y,transform.position.z);
        transform.position = Vector3.Lerp(transform.position,corrected,followingSpeeed);
    }
}
