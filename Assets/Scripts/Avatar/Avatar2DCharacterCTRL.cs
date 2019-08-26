using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class Avatar2DCharacterCTRL : MonoBehaviour
{
	public int raycastNumber = 4;
	public CapsuleCollider2D capsuleCollider;
	public Rigidbody2D rigidBody2D;
	public AvatarAbilities abilities;
	public GameObject[] spritesToFlip;
	public Animator[] spritesToAnimate;

	private float yOffset;
	private float xOffset;

	public bool playerIsGrounded;
	private BoxCollider2D colliderGround;
	private Vector3 cellPosition3;

	public float maxMovementSpeed = 300f;
	public float airControlMultiplier = 0.75f;
	private float bufferAirControl = 0;

	public bool pressedJump;

	public float jumpVelocity = 100f;
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;

	private Vector2 raycastOrigin;
	private double groundedMaximaleDistance = 0.02d;

	public GridLayout gridLayout;

	[HideInInspector]
	public bool isOnTyrolienne = false;
	public TyrolienneSC Tyroliennesc;
	public Vector2 endTyroliennePosition;
	private Vector2 startTyroliennePosition;


	public ParticleSystem jumpParticles;

	private float animationSpeedX;

	private bool isFacingRight = true;
	private int direction = -1;

	public float wallSlideSpeed = 6f;
	private bool isWallSliding;

	public float autoJumpVelocity = 10f;

	public bool sideRaycast0 = false;
	public bool sideRaycast1 = false;
	public bool sideRaycast2 = false;
	public bool sideRaycast3 = false;


    void Start()
    {
        yOffset = (capsuleCollider.size.y + 0.02f) / 2f;
        xOffset = (capsuleCollider.size.x + 0.02f) / 2f;

		FetchComponents();
    }

    void Update()
    {
			ThrowRaycastDown();
			BetterJump ();
			ThrowRaycastSide();
			foreach (Animator animator in spritesToAnimate){
				animator.SetFloat("SpeedX", Mathf.Abs(rigidBody2D.velocity.x / maxMovementSpeed));
				animator.SetFloat("SpeedY", rigidBody2D.velocity.y);
			}
    }

    void FixedUpdate()
    {
        if (isOnTyrolienne)
        {
        	TyrolienneMovement(endTyroliennePosition);
        }


    }
    public void TyrolienneOn (bool boolean, Vector2 endPosition)
    {
    	isOnTyrolienne = boolean;
    	endTyroliennePosition = endPosition;
    	startTyroliennePosition = this.transform.position;
    }

    public void TyrolienneMovement (Vector2 endPosition)
    {
    	rigidBody2D.velocity = Vector2.zero;
    	rigidBody2D.velocity = (endPosition - startTyroliennePosition) * 1f;
    }



    public void Jump()
    {
    	if (playerIsGrounded)
    	{
    		rigidBody2D.velocity += new Vector2 (rigidBody2D.velocity.x, jumpVelocity);
    		jumpParticles.Play();
    	}
		else
			abilities.WaterJump();
		isWallSliding = false;
    }


    void BetterJump ()
    {
    	if (rigidBody2D.velocity.y < 0)
    	{
    		rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    	} else if (rigidBody2D.velocity.y > 0 && !pressedJump)
    	{
    		rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    	}
    }



    public void SideMovement (float avatarDirection)
    {
    	if (!isOnTyrolienne && !isWallSliding)
    	{

			float desiredSpeed = avatarDirection * maxMovementSpeed * bufferAirControl;
    		rigidBody2D.velocity = new Vector2 (desiredSpeed, rigidBody2D.velocity.y);

    		GetRoundedDirection(avatarDirection);

    		if (avatarDirection != 0) {AutoSideJump();} 
    		
			if ((isFacingRight && avatarDirection < 0) || (!isFacingRight && avatarDirection > 0)) {Flip();}
    	}
    }

		void Flip() 
		{
			foreach (GameObject sprite in spritesToFlip) {
				Vector3 newScale = sprite.gameObject.transform.localScale;
				newScale.x *= -1;
				sprite.gameObject.transform.localScale = newScale;
			}
				isFacingRight = !isFacingRight;
			
		}

	void GetRoundedDirection (float avatarDirection)
	{
		if (avatarDirection > 0) {direction = 1;}
		if (avatarDirection < 0) {direction = -1;}

	}




    void OnTouchFloor ()
    {
  		bufferAirControl = 1;
  		jumpParticles.Play();
    }

    void TileFinder (RaycastHit2D hit)
    {
        Vector3Int cellPosition = gridLayout.WorldToCell(new Vector3 (hit.point.x, hit.point.y, 0));
        cellPosition3 = gridLayout.CellToWorld(cellPosition);
    }

    void AutoSideJump()
    {
    	if (sideRaycast1 && !sideRaycast2 && !sideRaycast3)
    	{
    		rigidBody2D.velocity = new Vector2 (0, autoJumpVelocity);
    		jumpParticles.Play();
    		sideRaycast1 = false;
    		sideRaycast3 = false;
    		sideRaycast2 = false;

	

    	}
    }




    void ThrowRaycastDown()
    {
        bool groundedBuffer = false; 

    	for (int i = 0; i < raycastNumber; ++i)
    	{
            
            raycastOrigin = new Vector2 (transform.position.x - xOffset + 0.01f + (capsuleCollider.size.x / (raycastNumber-1)) * i,
    		 	transform.position.y - yOffset);
    		
    		RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, -Vector2.up, 15f);

           Debug.DrawRay(raycastOrigin,-Vector2.up, Color.green);

            if (Mathf.Abs(hit.point.y - raycastOrigin.y) < groundedMaximaleDistance)
    		{
    			
    			groundedBuffer = true; 		
                TileFinder(hit);

            }
            else {
            
    			playerIsGrounded = false;
					foreach (Animator animator in spritesToAnimate)
    				animator.SetBool("Grounded", playerIsGrounded);

    			bufferAirControl = airControlMultiplier;

    		}

       		if (groundedBuffer  && playerIsGrounded == false)
       		{
       		
       			playerIsGrounded = true;
						foreach (Animator animator in spritesToAnimate)
      	 			animator.SetBool("Grounded", playerIsGrounded);
       			OnTouchFloor ();
       		}
    	}
	}

	void ThrowRaycastSide()
    {
        //bool wallBuffer = false;

    	for (int i = 0; i < raycastNumber; ++i)
    	{
            
            raycastOrigin = new Vector2 ((transform.position.x - xOffset * -direction),
    		 	transform.position.y - yOffset + (capsuleCollider.size.y / (raycastNumber-1)) * i);
    		
    		RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.right * direction, 15f);

    		Color colo = Color.white;
    		if (i == 0) {colo = Color.red;}
    		else if (i == 1) {colo = Color.yellow;}
    		else if (i == 2) {colo = Color.blue;}
    		else if (i == 3) {colo = Color.green;}

    		Debug.DrawRay(raycastOrigin, Vector2.right * direction, colo);

           

            if (Mathf.Abs(hit.point.x - raycastOrigin.x) < groundedMaximaleDistance)
    		{
    			if (i == 0) {sideRaycast0 = true;}
    			else if (i == 1) {sideRaycast1 = true;}
    			else if (i == 2) {sideRaycast2 = true;}
    			else if (i == 3) {sideRaycast3 = true;}
    	
    			//wallBuffer = true; 		
                WallSlide();

            }
            else {
            	if (i == 0) {sideRaycast0 = false;}
    			else if (i == 1) {sideRaycast1 = false;}
    			else if (i == 2) {sideRaycast2 = false;}
    			else if (i == 3) {sideRaycast3 = false;}
            	isWallSliding = false;
    			//wallBuffer = false;/*
    			//animator.SetBool("Grounded", playerIsGrounded);

    			//bufferAirControl = airControlMultiplier;
    		}/*

       		if (wallBuffer  && playerIsGrounded == false)
       		{
       			playerIsGrounded = true;
       			animator.SetBool("Grounded", playerIsGrounded);
       			OnTouchFloor ();
       		}*/
    	}
	}
	void WallSlide()
	{
		isWallSliding = false;

		if (!playerIsGrounded)
		{
			isWallSliding = true;
			if (rigidBody2D.velocity.y <= 0)
			{
				
				rigidBody2D.velocity = new Vector2 (0, -wallSlideSpeed);
			}
		}
	
	}

	void FetchComponents() {
		abilities = gameObject.GetComponent<AvatarAbilities>();
	}
}
