using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    public Font buttonFont;
    
    // Array to store rectangle for gui
    public Rect[] arrayOfRects;
    public Rect scoreRect;
    public Rect promptRect;

    public Camera mainCamera;

    public float cameraWidth;
    public float cameraHeight;

    // SpriteRenderer for colliders
    SpriteRenderer fireRateButtonSR;
    SpriteRenderer healthButtonSR;
    SpriteRenderer accelerationButtonSR;

    // Vectors to store rect positions
    public Vector3 fireRateButtonPos;
    public Vector3 healthButtonPos;
    public Vector3 accelerationButtonPos;

    // Start is called before the first frame update
    void Start()
    {
        fireRateButtonSR = GameObject.Find("FireRateButton").GetComponent<SpriteRenderer>();
        healthButtonSR = GameObject.Find("HealthButton").GetComponent<SpriteRenderer>();
        accelerationButtonSR = GameObject.Find("AccelerationButton").GetComponent<SpriteRenderer>();

        fireRateButtonPos = mainCamera.WorldToScreenPoint(fireRateButtonSR.transform.position);
        healthButtonPos = mainCamera.WorldToScreenPoint(healthButtonSR.transform.position); ;
        accelerationButtonPos = mainCamera.WorldToScreenPoint(accelerationButtonSR.transform.position); ;

        // Declaring rectangles for gui elements
        arrayOfRects = new Rect[3];
        arrayOfRects[0] = new Rect(fireRateButtonPos.x - 110, fireRateButtonPos.y - 50, 225, 80);
        arrayOfRects[1] = new Rect(healthButtonPos.x - 100, healthButtonPos.y - 50, 200, 80);
        arrayOfRects[2] = new Rect(accelerationButtonPos.x - 100, accelerationButtonPos.y - 50, 200, 110);
        scoreRect = new Rect(healthButtonPos.x - 100, healthButtonPos.y - 200, 200, 40);
        promptRect = new Rect(healthButtonPos.x - 100, healthButtonPos.y + 200, 200, 80);
    }

    // Update is called once per frame
    void Update()
    {
        // Returns to game when space is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    void OnGUI()
    {
        // Declaring a new font for gui
        GUI.skin.font = buttonFont;

        GUI.skin.box.fontSize = 30;

        // Declaring the background color as transparent
        GUI.backgroundColor = new Color(0.4f, 0.4f, 0.4f, 0);

        GUI.skin.box.wordWrap = true;

        // Creating GUI boxes
        GUI.Box(arrayOfRects[0], "UPGRADE FIRE RATE\nFIRE RATE: " + (GlobalObject.Instance.bulletTimeDelay / 1000)  + "s" + "\n(1000)");
        GUI.Box(arrayOfRects[1], "UPGRADE HEALTH\nHEALTH: " + GlobalObject.Instance.HP + "\n(2000)");
        GUI.Box(arrayOfRects[2], "UPGRADE ACCELERATION\nACCELERATION: " + (GlobalObject.Instance.accelerationRate * 1000) + "\n(1500)");
        GUI.Box(scoreRect, "RESOURCES: " + GlobalObject.Instance.resources);
        GUI.Box(promptRect, "PRESS SPACE TO RETURN TO GAME");
    }
}
