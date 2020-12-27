using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorUp;
    public GameObject doorDown;
    public float speed;

    private connection connection;
    void Start()
    {
        connection = GetComponent<connection>();
    }
    void Update()
    {
        if (connection.isActive)
        {
            if (doorUp.transform.localPosition.y < 3.8f)
                doorUp.transform.localPosition = new Vector2(doorUp.transform.localPosition.x, doorUp.transform.localPosition.y+speed);
            if(doorDown.transform.localPosition.y > -2.8f)
                doorDown.transform.localPosition = new Vector2(doorDown.transform.localPosition.x, doorDown.transform.localPosition.y-speed);
        }
        else
        {
            if (doorUp.transform.localPosition.y >  0.5f)
                doorUp.transform.localPosition = new Vector2(doorUp.transform.localPosition.x, doorUp.transform.localPosition.y-speed);
            if(doorDown.transform.localPosition.y < 0.5f)
                doorDown.transform.localPosition = new Vector2(doorDown.transform.localPosition.x, doorDown.transform.localPosition.y+speed);
        }
    }
}