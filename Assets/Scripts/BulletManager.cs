using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public List<GameObject> listOfBullets; // List to store and manage all bullets
    public GameObject bulletPrefab;
    public GameObject ship; // Reference to ship object
    public Stopwatch bulletTimer; // Stopwatch object for timekeeping between bullet spawns
    public float timeDelay; // The time delay between each bullet spawn
    float camWidthExtent;
    float camHeightExtent; // Will be used to determine when to despawn a bullet when its out of bounds

    // Start is called before the first frame update
    void Start()
    {
        listOfBullets = new List<GameObject>();
        bulletTimer = new Stopwatch();

        // Stores the camera height and width by accessing the ship
        camWidthExtent = ship.GetComponent<Vehicle>().totalCamWidth / 2;
        camHeightExtent = ship.GetComponent<Vehicle>().totalCamHeight / 2;

        // Loads in certain fields from global object
        LoadBullet();

        // If bullet time delay is less than 0 at start, its reset to the default value
        if (timeDelay == 0)
        {
            timeDelay = 800;
        }
        else if (timeDelay < 100)
        {
            timeDelay = 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
       // Resets timer if it has exceeded the time delay period
       if (bulletTimer.ElapsedMilliseconds >= timeDelay)
       {
           bulletTimer.Reset();
       }

       RemoveBullet();
    }

    // Method to spawn bullet
    public void SpawnBullet()
    {
        // Only spawns bullet if the timer is not running and is currently at 0s
        if (!bulletTimer.IsRunning && bulletTimer.ElapsedMilliseconds == 0)
        {
            listOfBullets.Add(Instantiate(bulletPrefab, ship.transform.position, Quaternion.identity));
            // Starts timer
            bulletTimer.Start();
        }
    }

    // Method that removes the bullet from the list and destroys it if it is out of the camera viewport bounds
    public void RemoveBullet()
    {
        for (int i = 0; i < listOfBullets.Count; i++)
        {
            if (listOfBullets[i].transform.position.x > camWidthExtent)
            {
                GameObject.Destroy(listOfBullets[i]);
                listOfBullets.RemoveAt(i);
                i--;
            }
            else if (listOfBullets[i].transform.position.x < -camWidthExtent)
            {
                GameObject.Destroy(listOfBullets[i]);
                listOfBullets.RemoveAt(i);
                i--;
            }
            else if (listOfBullets[i].transform.position.y > camHeightExtent)
            {
                GameObject.Destroy(listOfBullets[i]);
                listOfBullets.RemoveAt(i);
                i--;
            }
            else if (listOfBullets[i].transform.position.y < -camHeightExtent)
            {
                GameObject.Destroy(listOfBullets[i]);
                listOfBullets.RemoveAt(i);
                i--;
            }
        }
    }

    public void RemoveBulletAtIndex(int i)
    {
        Destroy(listOfBullets[i]);
        listOfBullets.RemoveAt(i);
    }

    // Method that saves certain fields to global object for upgrading
    public void SaveBullet()
    {
        GlobalObject.Instance.bulletTimeDelay = timeDelay;
    }

    // Method that loads fields from global object after upgrading
    public void LoadBullet()
    {
        timeDelay = GlobalObject.Instance.bulletTimeDelay;
    }
}
