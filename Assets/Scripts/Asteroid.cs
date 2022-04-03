using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AsteroidSize
{
    Large,
    Small
}

public class Asteroid : MonoBehaviour
{
    public Vector3 asteroidPos;
    public Vector3 asteroidVelocity;
    public Vector3 asteroidDirection;
    public float asteroidSpeed;
    public GameObject ship; // Reference to ship object
    public float camWidthExtent; // Cam width divided by 2 to get extent rather than total width
    public float camHeightExtent; // Cam height divided by 2 to get extent rather than total height
    public AsteroidSize asteroidSize;

    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.Find("spaceship");
        camWidthExtent = ship.GetComponent<Vehicle>().totalCamWidth / 2;
        camHeightExtent = ship.GetComponent<Vehicle>().totalCamHeight / 2;

        // Sets position of asteroid randomly
        // asteroidPos = new Vector3(Random.Range(-camWidthExtent, camWidthExtent), Random.Range(-camHeightExtent, camHeightExtent), 1);

        // Sets speed of asteroid
        asteroidSpeed = 0.01f;

        // Calculates asteroid velocity by multiplying the direction with the scalar speed
        asteroidVelocity = asteroidDirection * asteroidSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Wrap();
    }

    // Method for movement of asteroid
    public void Movement()
    {
        asteroidPos += asteroidVelocity;
        transform.position = asteroidPos;
    }

    // Method wraps the asteroid around the screen in case it moves out of bounds
    public void Wrap()
    {
        // All conditionals have a padding of 1 unit to prevent the wrap from looking jarring
        // and making sure the asteroid moves completely off screen before appearing on the other side

        // Conditional that checks x position of vehicle and wraps if it is to the "right" of the camera bounds
        if (asteroidPos.x > camWidthExtent + 0.7)
        {
            asteroidPos.x = -camWidthExtent - 0.7f;
        }

        // Conditional that checks x position of vehicle and wraps if it is to the "left" of the camera bounds
        if (asteroidPos.x < -camWidthExtent - 0.7)
        {
            asteroidPos.x = camWidthExtent + 0.7f;
        }

        // Conditional that checks y position of vehicle and wraps if it is "above" the camera bounds
        if (asteroidPos.y > camHeightExtent + 0.7)
        {
            asteroidPos.y = -camHeightExtent - 0.7f;
        }

        // Conditional that checks y position of vehicle and wraps if it is "below" the camera bounds
        if (asteroidPos.y < -camHeightExtent - 0.7)
        {
            asteroidPos.y = camHeightExtent + 0.7f;
        }
    }
}
