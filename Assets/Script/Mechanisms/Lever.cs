using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Sprite spriteUp;
    public Sprite spriteDown;
    public GameObject light;

    private connection connection;
    void Start()
    {
        connection = GetComponent<connection>();
    }


    void Update()
    {
        if (connection.isActive)
        {
            light.SetActive(true);
            sprite.sprite = spriteDown;
        }
        else
        {
            light.SetActive(false);
            sprite.sprite = spriteUp;
        }
    }
}
