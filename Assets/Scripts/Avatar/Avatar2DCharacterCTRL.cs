using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class Avatar2DCharacterCTRL : MonoBehaviour
{
	public int raycastNumber = 4;
	public BoxCollider2D boxCollider;
	public Rigidbody2D rigidBody2D;

	private float yOffset;
	private float xOffset;

	public bool playerIsGrounded;
	private BoxCollider2D colliderGround;

	public float movementSpeed = 300f;

	public bool pressedJump;

	public float jumpVelocity = 100f;
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 1f;

    void Start()
    {
        yOffset = (boxCollider.size.y + 0.001f) / 2f;
        xOffset = boxCollider.size.x / 2;

    }

    void Update()
    {
        ThrowRaycastDown();
        //BetterJump ();
        print (playerIsGrounded);


     
    }

    void LateUpdate()
    {
    	/*if (playerIsGrounded)
        	PlayerGroundedPositionSet();*/

    }



    public void AvatarJump()
    {
    	if (playerIsGrounded)
    	{
    		playerIsGrounded = false;
    		print ("jump");
    		rigidBody2D.velocity += new Vector2 (rigidBody2D.velocity.x, jumpVelocity);
    	}
    }


    void BetterJump ()
    {
    	if (rigidBody2D.velocity.y > 0)
    	{
    		rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    	} else if (rigidBody2D.velocity.y > 0 && !pressedJump)
    	{
    		rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    	}
    }



    public void SideMovement (int avatarDirection)
    {
    	print (avatarDirection);
    	rigidBody2D.velocity = new Vector2 (avatarDirection * movementSpeed, rigidBody2D.velocity.y);
    	this.gameObject.transform.position += new Vector3 (avatarDirection * 0.1f,0,0);
    }







    void PlayerGroundedPositionSet ()
    {
    	rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, 0);
    	//RigidbodyConstraints2D = FreezeRotationY;
    	//rigidBody2D.constraints = RigidbodyConstraints2D.FreezePositionY;

    	this.transform.position = new Vector2 (this.transform.position.x, colliderGround.gameObject.transform.position.y + colliderGround.size.y/2 + yOffset - 0.001f);
    }

    void OnTouchFloor ()
    {
    	PlayerGroundedPositionSet ();
  		playerIsGrounded = true;

    }




    void ThrowRaycastDown()
    {
    	bool groundedBuffer = false; 

    	for (int i = 0; i < raycastNumber; ++i)
    	{
    		RaycastHit2D hit = Physics2D.Raycast(new Vector2 (transform.position.x - xOffset + (boxCollider.size.x / (raycastNumber-1)) * i,
    		 	transform.position.y - yOffset), -Vector2.up, 0.01f);

    		Debug.DrawRay(new Vector2 (transform.position.x - xOffset + (boxCollider.size.x / (raycastNumber-1)) * i,
    		 	transform.position.y - yOffset), -Vector2.up * 0.1f, Color.red);


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
       	}

       	if (groundedBuffer == true && playerIsGrounded == false)
       	{
       		OnTouchFloor ();
       	}
    }


}
