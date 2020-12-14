using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{
    public float dumping = 1.5f;
    public Vector2 offcet = new Vector2(2f, 1f);
    public bool isleft;
    private Transform player;
    private int lastx;

    void Start()
    {
        offcet = new Vector2(Mathf.Abs(offcet.x), offcet.y);
        FindPlayer(isleft);
    }

    public void FindPlayer(bool playerIsLeft)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastx = Mathf.RoundToInt(player.position.x);
        if(playerIsLeft)
        {
            transform.position = new Vector3(player.position.x - offcet.x, player.position.y - offcet.y, transform.position.z);
        } else
        {
            transform.position = new Vector3(player.position.x + offcet.x, player.position.y + offcet.y, transform.position.z);
        }
    }
    
    void Update()
    {
        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > lastx) isleft = false; else if (currentX < lastx) isleft = true;
            lastx = Mathf.RoundToInt(player.position.x);

            Vector3 target;
            if (isleft)
            {
                target = new Vector3(player.position.x - offcet.x, player.position.y + offcet.y, transform.position.z);
            }
            else
            {
                target = new Vector3(player.position.x + offcet.x, player.position.y + offcet.y, transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}
