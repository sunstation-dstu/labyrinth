using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlantform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime; 

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            Physics2D.IgnoreLayerCollision(9, 12, false);
        }
        if (Input.GetKey(KeyCode.S))
        {
                Physics2D.IgnoreLayerCollision(9, 12, true);
        }

    }
}
