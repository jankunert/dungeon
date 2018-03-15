﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public float speed = 5.0f;
    public float sprintingSpeed = 3.0f;
    private bool sprinting=false;
    private float rotationChangingSpeed = 10000.0f;
    public float rotationSpeed = 1.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Quaternion destRotate;
    //private Vector3 destRotation;

    private CharacterController controller;
    //private Quaternion bordermax;
    //private Quaternion bordermin;
    public AudioClip aud;
    public float steplength = 0.4f;
    public float delay = 0;
    public float volume = 0.7f;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        destRotate = transform.rotation;
        //bordermax = Quaternion.Euler(80,0,0);
        //bordermin = Quaternion.Euler(-80,0,0);
    }

    // Update is called once per frame
    void Update()
    {
		Sprint ();
        //Quaternion actRotation = transform.rotation;
        //destRotation.x = actRotation.x + Input.GetAxis("MY") * rotationSpeed;
        //Vector3 view = destRotation;
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (sprinting)
        {
            moveDirection = moveDirection * (speed + sprintingSpeed);
        }
        else
        {
            moveDirection = moveDirection * speed;
        }

        moveDirection = transform.TransformDirection(moveDirection);
        controller.SimpleMove(moveDirection);
        destRotate.eulerAngles -= new Vector3(Input.GetAxis("MY") * rotationSpeed, 0, 0);
        //destRotate.SetLookRotation(view);
        destRotate.eulerAngles += new Vector3(0, Input.GetAxis("MouseX") * rotationSpeed, 0);


        /*if (Input.GetKey(KeyCode.Q))
        {
            destRotate.eulerAngles -= new Vector3(0, rotationSpeed, 0);

        }
        if (Input.GetKey(KeyCode.E))
        {
            destRotate.eulerAngles += new Vector3(0, rotationSpeed, 0);


        }*/

        


        float step = rotationChangingSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, destRotate, step);


        if (controller.velocity.sqrMagnitude > 0.2f)
        {
            if (delay > steplength)
            {
                AudioSource.PlayClipAtPoint(aud, transform.position, volume);
                
                if(sprinting==false)delay = 0;
                if (sprinting) delay = 0.1f;

            }
        }
        delay += Time.deltaTime;
    }
    private void Sprint()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprinting = true;
            AudioSource.PlayClipAtPoint(aud, transform.position, volume);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) {

               sprinting = false;
            delay = 0;
        }
    }

}