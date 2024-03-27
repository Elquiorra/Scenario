using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	private Rigidbody2D m_Rigidbody2D;
	public Animator animator;
	public AudioSource audSource;
	public AudioClip Run, Walk, Jump;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = 1f;	// How much to smooth out the movement
	private Vector3 m_Velocity = Vector3.zero;
	[SerializeField] private bool m_AirControl = true;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded.
	public bool m_Platformed = false;
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	
	public PlayerMovement player;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	private void Update(){
		animator.SetFloat("MovementSpeed", Mathf.Abs(m_Rigidbody2D.velocity.x));
		if(!m_Grounded){
			animator.SetBool("onAir", true);
		}
		else if(m_Grounded){
			animator.SetBool("onAir", false);
		}		
	}

	void OnCollisionStay2D(Collision2D other){
		if(other.gameObject.CompareTag("Platform")){
			m_Platformed = true;
		}
		else{
			m_Platformed = false;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.CompareTag("Platform")){
			m_Platformed = true;
		}
	}

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		audSource = GetComponent<AudioSource>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate(){
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++){
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();	
			}
		}
	}


	public void Move(float move, bool jump)
	{
		if (m_Grounded || m_AirControl)
		{
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);


			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			else if (move < 0 && m_FacingRight)
			{
				Flip();
			}
		}
		if (m_Grounded && jump){
			m_Grounded = false;
			m_Platformed = false;
			animator.SetTrigger("Jump");
			audSource.PlayOneShot(Jump, 0.2f);
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void Sound(AudioClip soundCode){
		audSource.clip = soundCode;
		if(!audSource.isPlaying){
			audSource.PlayOneShot(audSource.clip, 0.2f);
		}
	}		
}