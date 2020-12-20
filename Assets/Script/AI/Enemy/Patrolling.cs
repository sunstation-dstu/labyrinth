using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    /// <summary>
    /// Точки патрулирования
    /// </summary>
    public Transform[] points;
    /// <summary>
    /// Минимальной расстояние до цели, при котором считается, что она достигнута
    /// </summary>
    public float delta = 0.02f;
    
    [HideInInspector]
    public int currentPoint;

    private bool isPatrol;

    private void Start()
    {
        
    }
    
    private void Update()
    {
        if (Mathf.Abs(transform.position.x - points[currentPoint].position.x) < delta)
        {
            currentPoint = (currentPoint + 1) % points.Length;
        }
    }
}
