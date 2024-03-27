using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public CharacterController2D controller;
   public Animator animator;
   public float MovementSpeed = 120f;
   float xMovement;
   bool jump = false;

    void Update(){
        if(Input.GetKey(KeyCode.A)){
            xMovement = -20f;
        }
        else if(Input.GetKey(KeyCode.D)){
            xMovement = 20f;
        }
        else{
            xMovement = 0f;
        }
        
        if(Input.GetButtonDown("Jump")){
            jump = true;
        }
    }

    void FixedUpdate(){
        controller.Move(xMovement * Time.fixedDeltaTime, jump); jump = false;

        if(Mathf.Abs(xMovement) > 0){
            if(Input.GetKey(KeyCode.LeftShift)){
                controller.Move(xMovement * Time.fixedDeltaTime * 1.5f, jump);
                if(controller.m_Grounded == true){
                    controller.Sound(controller.Run);
                }
            }
            else{
                if(controller.m_Grounded == true){
                    controller.Sound(controller.Walk);
                }
            }
        }
    }
}