using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Animator animator;
	private Rigidbody2D rb; 
	public float speed = 5f;
	public float jumpForce = 1f;
	public bool isGrounded = false;
	public Transform groundCheck;
	public LayerMask groundLayer;
	
	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		float horizontal = Input.GetAxis ("Horizontal");
		rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
		animator.SetBool("IsRunning", horizontal != 0);
		
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded)
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			animator.SetTrigger("IsJumping");
		}

		animator.SetBool("IsGrounded", isGrounded);

	
		if (horizontal > 0)
		{
			transform.localScale = new Vector2(9, 10);
		}
		
		else if (horizontal < 0)
		{
			transform.localScale = new Vector2(-9, 10);
		}

	}


	void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsGroundLayer(collision.gameObject))
        {
            isGrounded = true;
        }
    }

	void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player leaves the ground
        if (IsGroundLayer(collision.gameObject))
        {
            isGrounded = false; 
        }
    }


	private bool IsGroundLayer(GameObject obj)
    {
        return ((1 << obj.layer) & groundLayer) != 0;
    }
}
