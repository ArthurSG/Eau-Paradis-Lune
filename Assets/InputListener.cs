using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputListener : MonoBehaviour
{
	public GameObject avatarGo;
	private Avatar2DCharacterCTRL avatarController;
	private AvatarAbilities avatarAbilities;

	private Vector3 touchPosition;

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
        AttackCheck();



    }

    void AttackCheck()
    {
    	if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;

            if (touch.phase == TouchPhase.Began)
            {
            	print ("1");
            	avatarAbilities.SlashInstantiate(touchPosition);
            }
            if (touch.phase == TouchPhase.Moved)
            {
            	print ("2");
            	avatarAbilities.SlashMove(touchPosition);
            }
            if (touch.phase == TouchPhase.Ended)
            {
            	print ("3");
            	avatarAbilities.SlashDestroy();
            }
        }

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
    	print ("onclik");

    	avatarController.AvatarJump();
    	if (!avatarController.playerIsGrounded)
    	{
			avatarAbilities.WaterJump();
    	}
    }

    public void OnReleaseJump ()
    {
    	//if (avatarController.playerIsGrounded)
 
    	print ("tta");
    	avatarController.AvatarJump();
    	
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
    	print ("aaaa");
    	pressedLeft = false;
    }

    public void OnReleaseRight()
    {
    	print ("aaaaeeee");
    	pressedRight = false;
    }
}
