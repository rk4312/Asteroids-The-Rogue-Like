using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 bulletPos;
    public Vector3 bulletDirection;
    public float speed;
    public GameObject ship;
    public float angleOfRotation;

    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.Find("spaceship");
        speed = 0.3f;
        bulletPos = ship.transform.position;
        bulletDirection = ship.GetComponent<Vehicle>().direction;
        angleOfRotation = ship.GetComponent<Vehicle>().angleOfRotation;
        transform.localScale *= 0.5f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleOfRotation));
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        DrawDebugLines();
    }

    public void Movement()
    {
        //Debug.Log("Movement method is called");

        bulletPos += bulletDirection * speed;
        transform.position = bulletPos;
    }

    public void DrawDebugLines()
    {
        // Bullet's direction vector (YELLOW)
        Debug.DrawLine(bulletPos, bulletPos + bulletDirection, Color.white);

        // Bullet's position vector from origin (BLUE)
        Debug.DrawLine(Vector3.zero, bulletPos, Color.cyan);
    }
}
