using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Le nom devrait être liée à la seul ressource que le scripte gère réellement : water
public class AvatarResources : MonoBehaviour
{
	// Nous savons déjà dque l'eau est une ressource. "water" me semble suffisant.
	public static float waterResource;
	public float maxWaterResources;
	public Text waterUIText;

	void Update() {
		waterUIText.text = Mathf.RoundToInt(waterResource).ToString();
	}

    public bool CanUseResources (float resourceNeededValue)
    {
    	if (resourceNeededValue <= waterResource)
    	{
    		waterResource -= resourceNeededValue;
    		return true;    		
    	}
    	else 
    		return false;	
    }
    
    //Appelée par les sources pour augmenter la value d'une resource. 
    public void ResourceValueModifier (float resourceValue)
    {
    	waterResource += resourceValue;
    	if (waterResource > maxWaterResources)
    	{
    		waterResource = maxWaterResources;
    	} else 
    	{
    		//animator.SetBool("TakeWater", true);
    	}
    }

    public void ResourceValueReset ()
    {
    	waterResource = 0;
    }

    
}
