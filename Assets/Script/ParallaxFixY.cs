using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxFixY : MonoBehaviour
{
    public GameObject camera1;
    public float coefficient = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, camera1.transform.position.y+ coefficient);
    }
}
