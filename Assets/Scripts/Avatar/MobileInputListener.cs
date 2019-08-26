using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputListener : KeyboardInputListener
{

    private float movementInput = 0f;
    
    protected override void TryMoving() {
        this.avatar.SideMovement(movementInput);
    }

    protected override void TryAttacking() {
        Vector3 touchPosition;
    	if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;

            if (touch.phase == TouchPhase.Began)
            {

            	avatar.abilities.SlashInstantiate(touchPosition);
    			foreach (Animator animator in avatar.spritesToAnimate)
            	    animator.SetBool("Slash", true);
            }
            if (touch.phase == TouchPhase.Moved)
            {
            
            	avatar.abilities.SlashMove(touchPosition);
            }
            if (touch.phase == TouchPhase.Ended)
            {
        
            	avatar.abilities.SlashDestroy();
    			foreach (Animator animator in avatar.spritesToAnimate)
            	    animator.SetBool("Slash", false);
            }
        }
    }

    // Functions called from UI button
    public void OnLeftButtonPressed () { movementInput -= 1f; }
    public void OnLeftButtonReleased() { movementInput += 1f; }
    public void OnRightButtonPressed() { movementInput += 1f; }
    public void OnRightButtonReleased() { movementInput -= 1f; }

    public void OnJumpButtonPressed()
    {
    	avatar.Jump();
    	avatar.pressedJump = true;
    }

    // Function called from UI button
    public void OnJumpButtonReleased ()
    {
    	avatar.pressedJump = false;
    }
}
