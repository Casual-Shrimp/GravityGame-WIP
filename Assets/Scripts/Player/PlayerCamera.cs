using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensitivity = 100f;
    public Transform player;
    float _xRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    { 
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -80, 80);

        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        player.Rotate(Vector3.up * mouseX);
        Debug.Log(sensitivity);
    }
}
