using UnityEngine;
using System.Collections;

public class RayScan : MonoBehaviour
{

    public string targetTag = "Player";
    public int rays = 6;
    public int distance = 15;
    public float angle = 20;
    public Vector2 offset;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(targetTag).transform;
    }
    
    
    bool GetRaycast(Vector2 dir)
    {
        bool result = false;
        Vector2 pos = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);;
        //var hit = Physics2D.Raycast(pos, dir, distance);
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, distance);
        if (hit.collider.gameObject.name == "TestSword (1)")
        {
            //print(hit.collider.gameObject.name);
            result = true;
            Debug.DrawLine(pos, hit.point, Color.green);
        }
        else
        {
            Debug.DrawRay(pos, dir * distance, Color.red);
        }

        return result;
    }

    bool RayToScan()
    {
        bool result = false;
        bool a = false;
        bool b = false;
        float j = 0;
        for (int i = 0; i < rays; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += angle * Mathf.Deg2Rad / rays;

            Vector2 dir = transform.TransformDirection(new Vector2(x,y));
            if (GetRaycast(dir)) a = true;

            if (x != 0)
            {
                dir = transform.TransformDirection(new Vector2(-x,y));
                if (GetRaycast(dir)) b = true;
            }
            print(dir);
        }

        if (a || b) result = true;
        return result;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < distance)
        {
            if (RayToScan())
            {
                
            }
            else
            {
                // Поиск цели...
            }
        }
    }
}