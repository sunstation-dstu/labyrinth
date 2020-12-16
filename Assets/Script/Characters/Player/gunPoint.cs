using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPoint : MonoBehaviour
{
    public float radius = 1;
    private float x;
    private float y;
    public float pointPos = 6.4f;

    void Update()
    {
        Vector2 cursor;
        cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var angle = Mathf.Atan2(cursor.y, cursor.x);
        var angleToDeg = angle * Mathf.Rad2Deg;
        if (angleToDeg < 90 && angleToDeg > -90)
        {
            x = radius * (Mathf.Cos(angle));
            y = radius * (Mathf.Sin(angle));
            transform.localPosition = new Vector2(x, y+pointPos);
        }
        else if (angleToDeg > 90f || angleToDeg < -90f)
        {
            x = radius * (Mathf.Cos(135-angle));
            y = radius * (Mathf.Sin(135-angle));
            transform.localPosition = new Vector2(x, y+pointPos);
        }
    }
}
