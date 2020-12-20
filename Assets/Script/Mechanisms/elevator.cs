using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public int upperUnit;

    public float maxSpeed = 2f;
    private float currentVelocity = 0.0f;
    private float upperSum;
    private float upperStay;
    public bool isActive = false;
    private connection connect;
    
    void Start()
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
    }
}
