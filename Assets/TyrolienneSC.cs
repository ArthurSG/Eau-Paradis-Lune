using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyrolienneSC : MonoBehaviour
{
	public GameObject avatar;
	public GameObject tyrolienneEndPoint;
	private Avatar2DCharacterCTRL avatarCharacterCTRL;

	private bool isAvatarOnMe;

	//public bool isOnTyrolienne = true;

    void Start()
    {
        avatarCharacterCTRL = avatar.GetComponent<Avatar2DCharacterCTRL>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
    	if (!avatarCharacterCTRL.isOnTyrolienne)
    	{
    		isAvatarOnMe = true;
    		avatarCharacterCTRL.isOnTyrolienne = true;
    		avatarCharacterCTRL.TyrolienneOn(true,new Vector2 (tyrolienneEndPoint.transform.position.x, tyrolienneEndPoint.transform.position.y));
    	}

    }

    void OnTriggerExit2D (Collider2D collider)
    {
    	if (isAvatarOnMe)
    	{
    		isAvatarOnMe = false;
    		avatarCharacterCTRL.isOnTyrolienne = false;
    		avatarCharacterCTRL.TyrolienneOn(false, tyrolienneEndPoint.transform.position);
    	}

    }
}
