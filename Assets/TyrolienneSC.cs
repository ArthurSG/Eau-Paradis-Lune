using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyrolienneSC : MonoBehaviour
{
	public GameObject avatar;
	public GameObject tyrolienneEndPoint;
	private Avatar2DCharacterCTRL avatarCharacterCTRL;

	public bool isOnTyrolienne = true;

    void Start()
    {
        avatarCharacterCTRL = avatar.GetComponent<Avatar2DCharacterCTRL>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D collider)
    {

    	//isOnTyrolienne = true;
    	avatarCharacterCTRL.TyrolienneOn(true,new Vector2 (tyrolienneEndPoint.transform.position.x, tyrolienneEndPoint.transform.position.y));
    	//avatarCharacterCTRL.TyrolienneMovement(tyrolienneEndPoint.transform.position);

    }

    void OnTriggerExit2D (Collider2D collider)
    {

    	avatarCharacterCTRL.TyrolienneOn(false, tyrolienneEndPoint.transform.position);

    }
}
