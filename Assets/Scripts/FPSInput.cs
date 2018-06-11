﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour {

   
    public float speed = 5.0f;
    private CharacterController _charController;
   
    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        float gravity = 0; 
        

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
       
        movement = transform.TransformDirection(movement);
        _charController.Move(movement* Time.deltaTime);
    }
}
