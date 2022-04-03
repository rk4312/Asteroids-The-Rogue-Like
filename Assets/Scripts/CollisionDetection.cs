using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // SpriteRender fields
    SpriteRenderer collider1SR;
    SpriteRenderer collider2SR;

    public Vector3 collider1Pos;
    public Vector3 collider2Pos;
    public float collider1Radius;
    public float collider2Radius;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AABBCollision(GameObject collider1, GameObject collider2)
    {
        // Stores the sprite renderer for each collider
        collider1SR = collider1.GetComponent<SpriteRenderer>();
        collider2SR = collider2.GetComponent<SpriteRenderer>();

        if (collider1SR.bounds.min.x < collider2SR.bounds.max.x &&
            collider1SR.bounds.max.x > collider2SR.bounds.min.x &&
            collider1SR.bounds.max.y > collider2SR.bounds.min.y &&
            collider1SR.bounds.min.y < collider2SR.bounds.max.y)
        {
            return true;
        }
        else
            return false;
    }

    public bool CircleCollision(GameObject collider1, GameObject collider2)
    {
        // Gets and stores sprite renderer of both colliders
        collider1SR = collider1.GetComponent<SpriteRenderer>();
        collider2SR = collider2.GetComponent<SpriteRenderer>();

        // Gets and stores position vectors of both colliders
        collider1Pos = collider1.transform.position;
        collider2Pos = collider2.transform.position;

        // Declares the radius for both colliders depending on which extent is larger x or y
        if (collider1SR.bounds.extents.x > collider1SR.bounds.extents.y)
            collider1Radius = collider1SR.bounds.extents.x;
        else
            collider1Radius = collider1SR.bounds.extents.y;

        if (collider2SR.bounds.extents.x > collider2SR.bounds.extents.y)
            collider2Radius = collider2SR.bounds.extents.x;
        else
            collider2Radius = collider2SR.bounds.extents.y;

        // Stores distance between the two colliders
        distance = Vector3.Distance(collider1Pos, collider2Pos);

        //Debug.Log("distance: " + distance + ", radius 1: " + collider1Radius + ", radius 2: " + collider2Radius);

        // Checks the distance squared against the sum of the 2 radii squared
        if (collider1Radius + collider2Radius > distance)
            return true;
        else
            return false;
    }
}
