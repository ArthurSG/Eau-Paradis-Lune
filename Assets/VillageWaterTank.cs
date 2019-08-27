using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageWaterTank : MonoBehaviour
{
	private BoxCollider2D boxCollider2D;

	public float waterResource;
	public float maxWaterResource;

	public GameObject avatar;
	public GameObject winPanel;
	private AvatarResources avatarResources;
	public Text ResourceText;


    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        avatarResources = avatar.GetComponent<AvatarResources>();
    }


    // void Update()
    // {
    // 		TextUpdate();
    // }

    // void TextUpdate()
    // {
    // 	ResourceText.text = waterResource + " / " + maxWaterResource;
    // }

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
    		winPanel.SetActive(true);
    	}
    }



}
