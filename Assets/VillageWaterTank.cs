using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageWaterTank : MonoBehaviour
{
	private BoxCollider2D boxCollider2D;

	public float waterResource;
	public float maxWaterResource;

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
    	GetWater ();
    }

    void GetWater ()
    {
    	avatarResources.ResourceValueReset();
    }

    void WinStateEvent ()
    {
    	if (waterResource >= maxWaterResource)
    	{
    		print ("you won !");
    	}
    }



}
