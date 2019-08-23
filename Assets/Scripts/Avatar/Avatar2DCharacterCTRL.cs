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

	private bool isOnTyrolienne = false;
	public TyrolienneSC Tyroliennesc;
	public Vector2 endTyroliennePosition;
	private Vector2 startTyroliennePosition;


	public ParticleSystem jumpParticles;

	public Animator animator;
	private float animationSpeedX;

	private bool isFacingRight = true;


    void Start()
    {
        yOffset = (capsuleCollider.size.y + 0.001f) / 2f;
        xOffset = capsuleCollider.size.x / 2;

		FetchComponents();
    }

    void Update()
    {
        ThrowRaycastDown();
        BetterJump ();
        animator.SetFloat("SpeedX", Mathf.Abs(rigidBody2D.velocity.x / maxMovementSpeed));
        animator.SetFloat("SpeedY", rigidBody2D.velocity.y);
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
    		rigidBody2D.velocity += new Vector2 (rigidBody2D.velocity.x, jumpVelocity);
			else
				abilities.WaterJump();
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
    	if (!isOnTyrolienne)
    	{
			float desiredSpeed = avatarDirection * maxMovementSpeed * bufferAirControl;
    		rigidBody2D.velocity = new Vector2 (desiredSpeed, rigidBody2D.velocity.y);

    		
			if ((isFacingRight && avatarDirection < 0) || (!isFacingRight && avatarDirection > 0)) {Flip();}
    	}
    }

		void Flip() 
		{
			Vector3 avatarScale = transform.localScale;
			avatarScale.x *= -1;
			transform.localScale = avatarScale;
			isFacingRight = !isFacingRight;
		}




    void OnTouchFloor ()
    {
  		bufferAirControl = 1;
    }

    void TileFinder (RaycastHit2D hit)
    {
        Vector3Int cellPosition = gridLayout.WorldToCell(new Vector3 (hit.point.x, hit.point.y, 0));
        cellPosition3 = gridLayout.CellToWorld(cellPosition);
    }




    void ThrowRaycastDown()
    {
        bool groundedBuffer = false; 

    	for (int i = 0; i < raycastNumber; ++i)
    	{
            
            raycastOrigin = new Vector2 (transform.position.x - xOffset + (capsuleCollider.size.x / (raycastNumber-1)) * i,
    		 	transform.position.y - yOffset);
    		
    		RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, -Vector2.up, 15f);

           

            if (Mathf.Abs(hit.point.y - raycastOrigin.y) < groundedMaximaleDistance)
    		{
    			groundedBuffer = true; 		
                TileFinder(hit);

            }
            else {
    			playerIsGrounded = false;
    			animator.SetBool("Grounded", playerIsGrounded);

    			bufferAirControl = airControlMultiplier;
    		}

       		if (groundedBuffer  && playerIsGrounded == false)
       		{
       			playerIsGrounded = true;
       			animator.SetBool("Grounded", playerIsGrounded);
       			OnTouchFloor ();
       		}
    	}
	}

	void FetchComponents() {
		animator = GetComponentInChildren<Animator>();
		abilities = gameObject.GetComponent<AvatarAbilities>();
	}
}
