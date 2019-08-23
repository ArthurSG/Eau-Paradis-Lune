using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    public GameObject avatar;
    public int eauVolée;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }


    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collidedCollider)
    {
        if (collidedCollider.gameObject.tag == "Slash")
        {
            Die();
        }
        else
        {
            StealWater();
        }
    }

    void StealWater()
    {
        if (AvatarResources.waterResource > eauVolée)
            avatar.GetComponent<AvatarResources>().ResourceValueModifier(-eauVolée);
        else
            avatar.GetComponent<AvatarResources>().ResourceValueReset();

        print(AvatarResources.waterResource);
    }

    void Die()
    {
        Destroy(this);
    }
}
