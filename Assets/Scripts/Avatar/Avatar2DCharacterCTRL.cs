using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class Avatar2DCharacterCTRL : MonoBehaviour
{
	public int raycastNumber = 4;
	public BoxCollider2D boxCollider;
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
        yOffset = (boxCollider.size.y + 0.001f) / 2f;
        xOffset = boxCollider.size.x / 2;

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
    	/*if (playerIsGrounded)
        	PlayerGroundedPositionSet();*/
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
    	//this.transform.position = Vector3.Lerp(this.transform.position, endPosition, 1f * Time.deltaTime);
    	rigidBody2D.velocity = (endPosition - startTyroliennePosition) * 1f;
    	//this.transform.position = 

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

    		this.gameObject.transform.position += new Vector3 (avatarDirection * 0.1f,0,0);
				if ((isFacingRight && avatarDirection < 0) || (!isFacingRight && avatarDirection > 0)) {
					Flip();
				}
    	}
    }

		void Flip() 
		{
			Vector3 avatarScale = transform.localScale;
			avatarScale.x *= -1;
			transform.localScale = avatarScale;
			isFacingRight = !isFacingRight;
		}







    void PlayerGroundedPositionSet ()
    {
    	//rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, 0);
        //RigidbodyConstraints2D = FreezeRotationY;
        //rigidBody2D.constraints = RigidbodyConstraints2D.FreezePositionY;

        //COLLIDER GROUND WIP
        //this.transform.position = new Vector2 (this.transform.position.x, colliderGround.gameObject.transform.position.y + colliderGround.size.y/2 + yOffset - 0.001f);
        //this.transform.position = new Vector2 (this.transform.position.x, cellPosition3.y + yOffset - 0.001f);
    }

    void OnTouchFloor ()
    {
  		bufferAirControl = 1;
    }

    void TileFinder (RaycastHit2D hit)
    {
        //gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        Vector3Int cellPosition = gridLayout.WorldToCell(new Vector3 (hit.point.x, hit.point.y, 0));
        cellPosition3 = gridLayout.CellToWorld(cellPosition);
    }




    void ThrowRaycastDown()
    {
        bool groundedBuffer = false; 

    	for (int i = 0; i < raycastNumber; ++i)
    	{
            
            raycastOrigin = new Vector2 (transform.position.x - xOffset + (boxCollider.size.x / (raycastNumber-1)) * i,
    		 	transform.position.y - yOffset);
    		
    		RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, -Vector2.up, 15f);

           
    		Debug.DrawRay(raycastOrigin, -Vector2.up * 5f, Color.red);

  

            if (Mathf.Abs(hit.point.y - raycastOrigin.y) < groundedMaximaleDistance)
    		{
    			groundedBuffer = true;

                //COLLIDER GROUND WIP
    		
                TileFinder(hit);

                //colliderGround = (TilemapCollider2D)hit.collider;
            }
            else {
    			playerIsGrounded = false;
    			animator.SetBool("Grounded", playerIsGrounded);

    			bufferAirControl = airControlMultiplier;
    		}



    		/*
  			if (hit.collider != null)
  			{
  				groundedBuffer = true;
  				colliderGround = (BoxCollider2D)hit.collider;
  				//OnTouchFloor();

  			}
  			else if (hit.collider == null)
  			{
  			
  				playerIsGrounded = false;
  			}
       	}*/

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
