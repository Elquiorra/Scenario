using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{   
    public CharacterController2D controller;
    public float MovementSpeed = 120f;
    float xMovement, yMovement;
    private Rigidbody2D m_Rigidbody2D;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = 0f;
	private Vector3 m_Velocity = Vector3.zero;

    void Awake(){
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(controller.m_Platformed == false && controller.m_Grounded == true){
            if(Input.GetKey(KeyCode.LeftArrow)){
                xMovement = -20f;
            }
            else if(Input.GetKey(KeyCode.RightArrow)){
                xMovement = 20f;
            }
            else{
                xMovement = 0f;
            }

            if(Input.GetKey(KeyCode.DownArrow)){
                yMovement = -20f;
            }
            else if(Input.GetKey(KeyCode.UpArrow)){
                yMovement = 20f;
            }
            else{
                yMovement = 0f;
            }
        }
        else{
            xMovement = yMovement = 0f;
        }
        
    }

    public void MovePlatform(float moveX, float moveY)
	{
		Vector3 targetVelocityX = new Vector2(moveX * 10f, m_Rigidbody2D.velocity.y);
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocityX, ref m_Velocity, m_MovementSmoothing);

        Vector3 targetVelocityY = new Vector2(m_Rigidbody2D.velocity.x, moveY * 10f);
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocityY, ref m_Velocity, m_MovementSmoothing);
	}

    void FixedUpdate(){
        MovePlatform(xMovement * Time.fixedDeltaTime, yMovement * Time.fixedDeltaTime);
    }
}
