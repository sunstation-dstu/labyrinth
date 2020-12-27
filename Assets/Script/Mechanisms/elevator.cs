using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public int upperUnit;

    public float maxSpeed = 2f;
    private float currentVelocity = 0.0f;
    [HideInInspector]
    public float upperSum;
    [HideInInspector]
    public float upperStay;
    public bool isActive = false;
    private connection connect;
    [HideInInspector]
    public float direction;

    void Start()
    {
        upperStay = transform.position.y;
        upperSum = upperStay + upperUnit;
        connect = GetComponent<connection>();
        direction = upperSum;
    }
    
    void Update()
    {
        if (connect.isActive && !isActive)
            isActive = true;
        if (isActive)
            transform.position = new Vector2(transform.position.x,
                Mathf.SmoothDamp(transform.position.y, direction, ref currentVelocity, 1f, maxSpeed));
        if(transform.position.y > upperSum - 0.005f && direction == upperSum)
        {
            isActive = false;
            transform.position = new Vector2(transform.position.x, Mathf.Round(transform.position.y*10)/10);
            direction = upperStay;
        } else if (transform.position.y < upperStay + 0.005f && direction == upperStay)
        {
            isActive = false;
            transform.position = new Vector2(transform.position.x, Mathf.Round(transform.position.y * 10) / 10);
            direction = upperSum;
        }
    }
}
/*void Start()
    {
        upperStay = transform.position.y;
        upperSum = upperStay + upperUnit;
        connect = GetComponent<connection>();
    }
    
    void Update()
    {
        if (connect.isActive)
            isActive = true;
        if (isActive)
            transform.position = new Vector2(transform.position.x,
                Mathf.SmoothDamp(transform.position.y, upperSum, ref currentVelocity, 1f, maxSpeed));
        else
            transform.position = new Vector2(transform.position.x,
                Mathf.SmoothDamp(transform.position.y, upperStay, ref currentVelocity, 1f, maxSpeed));
        if(transform.position.y > upperSum - 0.005f)
        {
            isActive = false;
            transform.position = new Vector2(transform.position.x, Mathf.Round(transform.position.y*10)/10);
        }
    }*/
