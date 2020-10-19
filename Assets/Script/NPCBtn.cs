using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBtn : MonoBehaviour
{
    public GameObject Btn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Btn.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
           Btn.SetActive(false);
    }
}
