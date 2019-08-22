using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarResources : MonoBehaviour
{
	public static float waterResource;
	public float maxWaterResources;


    void Start()
    {
        
    }


    void Update()
    {
        print (waterResource);
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
    	}
    }

    public void ResourceValueReset ()
    {
    	waterResource = 0;
    }

    
}
