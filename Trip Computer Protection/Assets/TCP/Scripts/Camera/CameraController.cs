using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float panSpeed = 30f;
    [SerializeField] float panBorderThickness = 10f;
    [SerializeField] float scrollSpeed = 1000f;
    [SerializeField] float minY = 10f;
    [SerializeField] float maxY = 80f;
    bool doMovement = true;

     void Update()
     {
        MoveCamera();
        ZoomCamera();
        RotateCamera();
     }
    
     void MoveCamera()
     {
        if(Input.GetKeyDown(KeyCode.Escape)) { doMovement = false; }
    
        if (doMovement == false) return;
    
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward.ToIso() * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.back.ToIso() * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right.ToIso() * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left.ToIso() * panSpeed * Time.deltaTime, Space.World);
        }
     }
    
     void ZoomCamera()
     {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if( scroll > 0  && transform.position.y > minY)
        {
            Zoom(scroll);
        }
        else if( scroll < 0 && transform.position.y < maxY)
        {
            Zoom(scroll);
        }
     }
    void Zoom(float scroll)
    {
        transform.Translate(Vector3.forward * Time.deltaTime * scroll * scrollSpeed, Space.Self);
    }

    void RotateCamera()
    {
        if (Input.GetKeyDown("q"))
        {
        transform.Rotate(Vector3.up * 90, Space.World);
        }
        else if(Input.GetKeyDown("e"))
        {
            transform.Rotate(Vector3.down * 90, Space.World);
        }
    }
}
