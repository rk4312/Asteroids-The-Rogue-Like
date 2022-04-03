using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    // List of asteroids
    public List<GameObject> listOfAsteroids;

    // Array of asteroid prefabs to choose from
    public GameObject[] arrayOfPrefabs;

    // Stopwatch for timing spawns
    Stopwatch spawnTimer;

    // Spaceship prefab
    public GameObject spaceship;
    
    // Bools for button pressed
    bool pressed1;
    bool pressed2;
    bool shipCollisionDetected;

    Vector3 randomPosition;

    public float camWidthExtent; // Cam width divided by 2 to get extent rather than total width
    public float camHeightExtent; // Cam height divided by 2 to get extent rather than total height

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = new Stopwatch();
        listOfAsteroids = new List<GameObject>();

        camWidthExtent = spaceship.GetComponent<Vehicle>().totalCamWidth / 2;
        camHeightExtent = spaceship.GetComponent<Vehicle>().totalCamHeight / 2;

        pressed1 = false;
        pressed2 = true; // setting this to true to enable circle collision by default

        // Spawning the initial three asteroids
        for (int i = 0; i < 3; i++)
        {
            randomPosition = new Vector3(Random.Range(-camWidthExtent, camWidthExtent), Random.Range(-camHeightExtent, camHeightExtent), 0);

            listOfAsteroids.Add(Instantiate(arrayOfPrefabs[i], randomPosition, Quaternion.identity));

            // Passes the random position onto the asteroid script
            listOfAsteroids[i].GetComponent<Asteroid>().asteroidPos = randomPosition;

            // Defines the asteroid's direction randomly
            listOfAsteroids[i].GetComponent<Asteroid>().asteroidDirection = Random.insideUnitCircle.normalized;

            // Defines the asteroid's size
            listOfAsteroids[i].GetComponent<Asteroid>().asteroidSize = AsteroidSize.Large;
        }

        // Starts timer
        spawnTimer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // Calls on method for collision with ships
        ShipCollision();

        // Calls on asteroid spawn method
        AsteroidSpawn();

        // Calls on method for collisions with bullets
        BulletCollision();
    }

    public void AsteroidSpawn()
    {
        // Spawns asteroid every 3 seconds
        if (spawnTimer.ElapsedMilliseconds >= 3000)
        {
            // Random number to choose prefab
            int randomPrefabIndex = Random.Range(0, 2);

            randomPosition = new Vector3(Random.Range(-camWidthExtent, camWidthExtent), Random.Range(-camHeightExtent, camHeightExtent), 0);

            // Instantiates a new asteroid and adds it to the list
            listOfAsteroids.Add(Instantiate(arrayOfPrefabs[randomPrefabIndex], randomPosition, Quaternion.identity));

            // Passes the random position onto the asteroid script
            listOfAsteroids[listOfAsteroids.Count - 1].GetComponent<Asteroid>().asteroidPos = randomPosition;

            // Defines the asteroid's size
            listOfAsteroids[listOfAsteroids.Count - 1].GetComponent<Asteroid>().asteroidSize = AsteroidSize.Large;

            // Sets direction of asteroid randomly
            listOfAsteroids[listOfAsteroids.Count - 1].GetComponent<Asteroid>().asteroidDirection = Random.insideUnitCircle.normalized;

            // Resets and starts the timer again
            spawnTimer.Reset();
            spawnTimer.Start();
        }
    }

    public void ShipCollision()
    {
        // Checks for input and sets their bool accordingly
        //if (Input.GetKey(KeyCode.Alpha1))
        //{
        //    pressed1 = true;
        //    pressed2 = false;
        //}
        //else if (Input.GetKey(KeyCode.Alpha2))
        //{
        //    pressed1 = false;
        //    pressed2 = true;
        //}

        // Runs the appropriate collision method based on which button is pressed
        //if(pressed1)
        //{
        //    shipCollisionDetected = false;

        //    // For loops through the asteroid list and checks for AABB collision against spaceship
        //    for (int i = 0; i < listOfAsteroids.Count; i++)
        //    {
        //        if (gameObject.GetComponent<CollisionDetection>().AABBCollision(spaceship, listOfAsteroids[i]))
        //        {
        //            // If collision returns true a color change is declared
        //            listOfAsteroids[i].GetComponent<SpriteRenderer>().color = Color.red;
        //            shipCollisionDetected = true;
        //        }
        //        else
        //        {
        //            // If collision returns false the color change is reset
        //            listOfAsteroids[i].GetComponent<SpriteRenderer>().color = Color.white;
        //        }
        //    }

        //    if (shipCollisionDetected)
        //    {
        //        spaceship.GetComponent<SpriteRenderer>().color = Color.red;
        //    }
        //    else
        //    {
        //        spaceship.GetComponent<SpriteRenderer>().color = Color.white;
        //    }
        //}

        if (pressed2)
        {
            shipCollisionDetected = false;

            // For loops through the asteroid list and checks for circle collision against spaceship
            for (int i = 0; i < listOfAsteroids.Count; i++)
            {
                if (gameObject.GetComponent<CollisionDetection>().CircleCollision(spaceship, listOfAsteroids[i]))
                {
                    // If collision returns true a color change is declared
                    listOfAsteroids[i].GetComponent<SpriteRenderer>().color = Color.red;
                    shipCollisionDetected = true;
                }
                else
                {
                    // If collision returns false the color change is reset
                    listOfAsteroids[i].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }

            if (shipCollisionDetected)
            {
                spaceship.GetComponent<SpriteRenderer>().color = Color.red;

                // Reduces health of ship
                if (!spaceship.GetComponent<Vehicle>().invincible)
                {
                    spaceship.GetComponent<Vehicle>().health--;
                }

                // Sets invincible of spaceship to true to prevent further damage
                spaceship.GetComponent<Vehicle>().invincible = true;
            }
            else
            {
                spaceship.GetComponent<SpriteRenderer>().color = Color.white;

                // Sets the invincible bool of ship to false once there are no collisions detected
                spaceship.GetComponent<Vehicle>().invincible = false;
            }
        }
    }

    // Method for bullet collision (Bounding circle based)
    public void BulletCollision()
    {
        // Loops thorugh all the bullets for each asteroid in the list in a nested for loop
        for (int i = 0; i < listOfAsteroids.Count; i++)
        {
            for (int j = 0; j < GameObject.Find("BulletManager").GetComponent<BulletManager>().listOfBullets.Count; j++)
            {
                // UnityEngine.Debug.Log("Enters conditional when there's more than one bullet");

                // Conditional to check if a collision is detected between an asteroid and a bullet
                // If it is then we determine the size of the asteroid the bullet collided with
                if (gameObject.GetComponent<CollisionDetection>().
                    CircleCollision(GameObject.Find("BulletManager").GetComponent<BulletManager>().listOfBullets[j], listOfAsteroids[i]))
                {
                    // UnityEngine.Debug.Log("collision detected between bullet and asteroid");

                    // Destroys bullet and decrements j
                    GameObject.Find("BulletManager").GetComponent<BulletManager>().RemoveBulletAtIndex(j);
                    j--;

                    // Checks asteroid's size and determines what to do
                    if (listOfAsteroids[i].GetComponent<Asteroid>().asteroidSize == AsteroidSize.Large)
                    {
                        // Spawns 2 smaller asteroids, destroys parent asteroid and decrements i
                        for (int k = 0; k < 2; k++)
                        {
                            // Adds to new asteroids to the list
                            listOfAsteroids.Add(Instantiate(arrayOfPrefabs[k], listOfAsteroids[i].transform.position, Quaternion.identity));

                            // Defines position of new asteroid to the parent asteroid's position
                            listOfAsteroids[listOfAsteroids.Count - 1].GetComponent<Asteroid>().asteroidPos = new Vector3(
                                Random.Range(listOfAsteroids[i].transform.position.x - 0.2f, listOfAsteroids[i].transform.position.x + 0.2f), 
                                Random.Range(listOfAsteroids[i].transform.position.y - 0.2f, listOfAsteroids[i].transform.position.y + 0.2f), 0);

                            // Defines direction of new asteroid similar to the parent's direction
                            listOfAsteroids[listOfAsteroids.Count - 1].GetComponent<Asteroid>().asteroidDirection = new Vector3(
                                // X direction
                                Random.Range(listOfAsteroids[i].GetComponent<Asteroid>().asteroidDirection.x - 0.4f, listOfAsteroids[i].GetComponent<Asteroid>().asteroidDirection.x + 0.4f),
                                // Y direction
                                Random.Range(listOfAsteroids[i].GetComponent<Asteroid>().asteroidDirection.y - 0.4f, listOfAsteroids[i].GetComponent<Asteroid>().asteroidDirection.y + 0.4f), 0);

                            // Changes the scale of the asteroid prefab
                            listOfAsteroids[listOfAsteroids.Count - 1].transform.localScale *= 0.5f;

                            // Defines the asteroid's size
                            listOfAsteroids[listOfAsteroids.Count - 1].GetComponent<Asteroid>().asteroidSize = AsteroidSize.Small;
                        }

                        Destroy(listOfAsteroids[i]);
                        listOfAsteroids.RemoveAt(i);
                        i--;

                        // Adds to the score
                        spaceship.GetComponent<Vehicle>().score += 100;
                    }
                    // Runs if asteroid size is small
                    else
                    {
                        Destroy(listOfAsteroids[i]);
                        listOfAsteroids.RemoveAt(i);
                        i--;

                        // Adds to the score
                        spaceship.GetComponent<Vehicle>().score += 150;
                    }
                    // Breaks out of the loop if the asteroid and bullet have been destroyed
                    break;
                }
            }
        }
    }
}
