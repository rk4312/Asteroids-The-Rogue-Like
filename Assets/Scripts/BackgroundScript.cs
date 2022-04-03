using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float cameraWidth;
    float cameraHeight;
    float scale;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = Camera.main.aspect * cameraHeight;

        // Sets the scale to camera width divided by the width of the prefab
        scale = cameraWidth / spriteRenderer.bounds.size.x;

        transform.localScale *= scale;

        // Keeps the background from being destroyed when there is a scene change
        DontDestroyOnLoad(GameObject.Find("BackgroundManager"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
