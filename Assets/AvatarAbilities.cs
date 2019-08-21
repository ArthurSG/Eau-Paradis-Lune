using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAbilities : MonoBehaviour
{
	private AvatarResources avatarResources;
	private Avatar2DCharacterCTRL avatarController;
	private bool isAvatarGrounded;

	public GameObject slashGameObjectToInstantiate;
	private GameObject slashGo;

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

    public void SlashInstantiate(Vector3 InputPosition)
    {	
    	slashGo = Instantiate(slashGameObjectToInstantiate, InputPosition, Quaternion.identity);

    }

    public void SlashMove(Vector3 InputPosition)
    {
    	slashGo.transform.position = InputPosition;
    }

    public void SlashDestroy()
    {
    	Destroy(slashGo);
    }


    public void WaterJump ()
    {
    	if (avatarResources.CanUseResources(waterJumpCost))
    	{
    		avatarController.rigidBody2D.velocity = new Vector2 (avatarController.rigidBody2D.velocity.x, avatarController.jumpVelocity);
    	}
    }
}
