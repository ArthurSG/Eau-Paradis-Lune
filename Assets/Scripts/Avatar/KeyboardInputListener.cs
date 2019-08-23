using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInputListener : MonoBehaviour
{
	public Avatar2DCharacterCTRL avatar;

    // Used for the jump axis (the keyboard arrows).
    private bool isJumpPressed = false;

    void Update()
    {
        TryJumping();   
        TryMoving();
        TryAttacking();
    }

    protected virtual void TryJumping() {
        JumpWithAxis();
        JumpWithButtons();
    }

    void JumpWithAxis() {
        if (Input.GetAxis("Vertical") > 0 && !isJumpPressed) {
            isJumpPressed = true;
            avatar.Jump();
    	    avatar.pressedJump = true;
        }
        else if (Input.GetAxis("Vertical") == 0 && isJumpPressed) {
            isJumpPressed = false;
    	    avatar.pressedJump = false;
        }
    }

    void JumpWithButtons() {
        if (Input.GetButtonDown("Jump")) {
            avatar.Jump();
    	    avatar.pressedJump = true;
        }
        else if (Input.GetButtonUp("Jump")) {
    	    avatar.pressedJump = false;
        }
    }

    protected virtual void TryAttacking()
    {
        // TODO attacking with a mouse

    }

    protected virtual void TryMoving () {
        avatar.SideMovement(Input.GetAxis("Horizontal"));
    }    
}
