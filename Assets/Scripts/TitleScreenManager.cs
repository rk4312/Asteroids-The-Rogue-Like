using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public float cameraWidth;
    public float cameraHeight;
    public Font titleFont;

    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = Camera.main.aspect * cameraHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    void OnGUI()
    {
        // Declaring a new font for gui
        GUI.skin.font = titleFont;

        GUI.skin.box.fontSize = 300;

        // Declaring the background color as transparent
        GUI.backgroundColor = new Color(0.4f, 0.4f, 0.4f, 0);

        GUI.skin.box.wordWrap = true;

        GUI.Box(new Rect(85, 200, 1750, 500), "ASTEROIDS:\nTHE ROGUE-LIKE");

        GUI.skin.box.fontSize = 30;
        
        GUI.Box(new Rect(835, 750, 250, 40), "PRESS SPACE TO START");
    }
}
