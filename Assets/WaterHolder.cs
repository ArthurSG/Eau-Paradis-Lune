using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHolder : MonoBehaviour
{
	private BoxCollider2D boxCollider2D;
	public bool infinitWater;
	private bool isEmpty;

	public float waterResourceValueToGive;

	public GameObject avatar;
	private AvatarResources avatarResources;


    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        avatarResources = avatar.GetComponent<AvatarResources>();
    }


    void Update()
    {

    }

    void OnTriggerStay2D (Collider2D collidedCollider)
    {
    	if (!isEmpty)
    		{
    			GiveWater ();
    			if (!infinitWater)
    				MakeEmpty();
    		}
    	
    }

    void GiveWater ()
    {
    	avatarResources.ResourceValueModifier(waterResourceValueToGive);
    }

    void MakeEmpty ()
    {
    	isEmpty = true;
    }

}
