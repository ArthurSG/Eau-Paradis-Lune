using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraIA : MonoBehaviour
{
    bool characterIsFalling;
    float fallingTime;
    public float vitesseCamera;

    public GameObject focusPoint;

    public const float COORDONNEE_MAX_X = 2;
    public const float COORDONNEE_MAX_Y = 2f;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetFocusPoint();
        GoToFocusPoint();
    }

    void GoToFocusPoint()
    {
        Vector2 direction = new Vector2(focusPoint.transform.position.x - transform.position.x, focusPoint.transform.position.y - transform.position.y);
        GetComponent<Rigidbody2D>().velocity = direction * vitesseCamera;
    }

    void SetFocusPoint()
    {
        //focusPoint = GetAvatarPosition() + new Vector2(focusPoint.transform.position.x * COORDONNEE_MAX_X, focusPoint.transform.position.y * COORDONNEE_MAX_Y);
    }

    Vector2 GetAvatarPosition()
    {
        return new Vector2(transform.parent.position.x, transform.parent.position.y);
    }
}
