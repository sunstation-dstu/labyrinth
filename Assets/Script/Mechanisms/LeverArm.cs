using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverArm : MonoBehaviour
{
    public bool isActive = false;
    private bool youIn = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        youIn = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        youIn = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && youIn)   isActive = !isActive;

        if (isActive) transform.rotation = Quaternion.Euler(0,0,180);
        else transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
