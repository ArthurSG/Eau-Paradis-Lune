using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAbilities : MonoBehaviour
{
	private AvatarResources avatarResources;
	private Avatar2DCharacterCTRL avatarController;
	private bool isAvatarGrounded;

	public float waterJumpCost = 5;
	public float waterSlashCost = 7;

    void Start()
    {
    	avatarResources = GetComponent<AvatarResources>();   
    	avatarController = GetComponent<Avatar2DCharacterCTRL>(); 
    	isAvatarGrounded = avatarController.playerIsGrounded;
    }

    void Update()
    {
        
    }

    public void WaterJump ()
    {
    	if (avatarResources.CanUseResources(waterJumpCost))
    	{
    		avatarController.rigidBody2D.velocity = new Vector2 (avatarController.rigidBody2D.velocity.x, avatarController.jumpVelocity);
    	}
    }
}
