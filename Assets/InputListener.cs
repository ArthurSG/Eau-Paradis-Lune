using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputListener : MonoBehaviour
{
	public GameObject avatarGo;
	private Avatar2DCharacterCTRL avatarController;
	private AvatarAbilities avatarAbilities;

	private bool pressedLeft;
	private bool pressedRight;
	//public bool pressedJump;

    void Start()
    {
     	avatarController = avatarGo.GetComponent<Avatar2DCharacterCTRL>(); 
     	avatarAbilities = avatarGo.GetComponent<AvatarAbilities>();  
    }

    void Update()
    {
        SendDirection();
    }

    void SendDirection ()
    {
    	if (pressedLeft)
    		avatarController.SideMovement(-1);
    	else if (pressedRight)
    		avatarController.SideMovement(1);
    }
    public void OnClickJump()
    {
    	avatarController.pressedJump = true;

    	if (avatarController.playerIsGrounded)
    	{
    		avatarController.AvatarJump();
    	} 
    	else if (!avatarController.playerIsGrounded)
    	{
			avatarAbilities.WaterJump();
    	}
    }

    public void OnReleaseJump ()
    {
    	avatarController.pressedJump = false;
    }


    public void OnClickLeft ()
    {
    	pressedLeft = true;
    	//avatarController.SideMovement(-1);
    }

    public void OnClickRight()
    {
    	pressedRight = true;
    	//avatarController.SideMovement(1);
    }

    public void OnReleaseLeft()
    {
    	pressedLeft = false;
    }

    public void OnReleaseRight()
    {
    	pressedRight = false;
    }
}
