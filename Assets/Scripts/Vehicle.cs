using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vehicle : MonoBehaviour
{
    public float accelRate; // Rate at which acceleration increases
    public Vector3 vehiclePosition; // Position of the vehicle
    public Vector3 direction; // Direction the vehicle is pointing in
    public Vector3 velocity; // Velocity vector that is applied to the position
    public Vector3 acceleration; // Acceleration vector that is applied to the velocity
    public float angleOfRotation; // Angle that vehicle prefab is rotated by on screen
    public float maxSpeed; // Max speed to prevent vehicle from going too fast as acceleration increases
    public Camera mainCamera; // Camera object that represents main camera
    public float totalCamHeight; // Total camera height
    public float totalCamWidth; // Total camera width
    public GameObject BulletManagerObject; // Stores reference to bullet manager object in scene
    public BulletManager bulletManagerScript; // Stores reference to bullet manager script
    public Texture2D livesIndicator; // Texture used to indicate lives
    public int healthMax;
    public int health;
    public bool invincible;
    public float score; // Float that stores the score
    

    private void Awake()
    {
        totalCamHeight = mainCamera.orthographicSize * 2f;
        totalCamWidth = totalCamHeight * mainCamera.aspect;
    }

    // Start is called before the first frame update
    void Start()
    {
        vehiclePosition = Vector3.zero;
        direction = Vector3.right;
        velocity = Vector3.zero;
        score = 0;
        BulletManagerObject = GameObject.Find("BulletManager");
        bulletManagerScript = BulletManagerObject.GetComponent<BulletManager>();
        invincible = false;

        // Loads vehicle stats from global object
        LoadVehicle();

        // If accelRate is set to 0 at start, it is reset to the default value
        if (accelRate <= 0)
        {
            accelRate = 0.0009f;
        }

        // If healthMax is set to 0 at start, it is reset to the default value
        if (healthMax <= 0)
        {
            healthMax = 3;
        }

        health = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        RotateVehicle();
        Drive();
        Wrap();
        SetTransform();
        DrawDebugLines();
        Shoot();

        if (health <= 0)
        {
            SaveVehicle();
            GameObject.Find("BulletManager").GetComponent<BulletManager>().SaveBullet();
            Destroy(gameObject);
            SceneManager.LoadScene("UpgradeScene");
        }
    }

    public void SetTransform()
    {
        // Vehicle is rotated by this quaternion
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);

        // Vehicle's position is set as the vehiclePosition vector with velocity applied
        transform.position = vehiclePosition;
    }

    public void Drive()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Acceleration vector that holds direction and magnitude
            acceleration = accelRate * direction;

            // Accelration vector is added to velocity vector
            velocity += acceleration;

            // Caps max speed
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

            // Adds velocity vector to vehiclePosition
            vehiclePosition += velocity;
        }
        else
        {
            // Deceleration of velocity vector
            velocity = 0.98f * velocity;

            // Adds velocity vector to vehiclePosition
            vehiclePosition += velocity;
        }
    }

    public void RotateVehicle()
    {
        // Rotates the vehicle based on left or right arrow press
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angleOfRotation += 3;
            direction = Quaternion.Euler(0, 0, 3) * direction;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            angleOfRotation -= 3;
            direction = Quaternion.Euler(0, 0, -3) * direction;
        }
    }

    // Method wraps the vehicle around the screen in case it moves out of bounds
    public void Wrap()
    {
        // Conditional that checks x position of vehicle and wraps if it is to the "right" of the camera bounds
        if (vehiclePosition.x > totalCamWidth / 2)
        {
            vehiclePosition.x = -totalCamWidth / 2;
        }

        // Conditional that checks x position of vehicle and wraps if it is to the "left" of the camera bounds
        if (vehiclePosition.x < -totalCamWidth / 2)
        {
            vehiclePosition.x = totalCamWidth / 2;
        }

        // Conditional that checks y position of vehicle and wraps if it is "above" the camera bounds
        if (vehiclePosition.y > totalCamHeight / 2)
        {
            vehiclePosition.y = -totalCamHeight / 2;
        }

        // Conditional that checks y position of vehicle and wraps if it is "below" the camera bounds
        if (vehiclePosition.y < -totalCamHeight / 2)
        {
            vehiclePosition.y = totalCamHeight / 2;
        }
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Spawns a bullet on space button press
            bulletManagerScript.SpawnBullet();
        }
    }

    void DrawDebugLines()
    {
        // Vehicle's velocity vector (RED)
        Debug.DrawLine(vehiclePosition, vehiclePosition + (velocity * 2000), Color.red);

        // Vehicle's direction vector (YELLOW)
        Debug.DrawLine(vehiclePosition, vehiclePosition + direction, Color.yellow);

        // Vehicle's position vector from origin (BLUE)
        Debug.DrawLine(Vector3.zero, vehiclePosition, Color.blue);
    }

    void OnGUI()
    {
        // Draws the lives indicator and score on gui
        GUI.backgroundColor = new Color(1, 1, 1, 0.2f);
        GUI.Box(new Rect((totalCamWidth / 2) + 0.5f, (totalCamHeight / 2) + 0.5f, 165, 50), "Lives remaining: \nResources: " + score);
        GUI.skin.box.fontSize = 20;
        GUI.skin.box.wordWrap = false;

        // Draws lives indicator texture onto GUI depending on the health field of vehicle
        for (int i = 0; i < health; i++)
        {
            GUI.DrawTexture(new Rect((totalCamWidth / 2 + 160) + 30 * i, (totalCamHeight / 2) + 5, 25, 25), livesIndicator);
        }
    }

    // Method to save certain fields to global object for upgrading
    public void SaveVehicle()
    {
        GlobalObject.Instance.HP = healthMax;
        GlobalObject.Instance.accelerationRate = accelRate;
        GlobalObject.Instance.resources = score;
    }

    // Method to load certain fields from global object to carry over upgrades
    public void LoadVehicle()
    {
        healthMax = GlobalObject.Instance.HP;
        accelRate = GlobalObject.Instance.accelerationRate;
        score = GlobalObject.Instance.resources;
    }
}