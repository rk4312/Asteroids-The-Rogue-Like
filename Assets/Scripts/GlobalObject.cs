using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObject : MonoBehaviour
{
    public static GlobalObject Instance;

    // Fields to carry over into upgrade scene
    public float accelerationRate;
    public int HP;
    public float bulletTimeDelay;
    public float resources;

    void Awake()
    {
        // This block of code makes sure there is only one instance of the global object
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
