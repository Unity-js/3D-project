using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Vector3 offset; 
    public float mouseSensitivity = 10f; 

    private float rotationY = 0f; 
    private float rotationX = 0f;

    void Update()
    {

        rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -30f, 30f); 


        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform.rotation = rotation;

        transform.position = target.position + rotation * offset;
    }
}
