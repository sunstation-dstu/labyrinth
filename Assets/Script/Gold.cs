using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{

   // Money cashCounter = golden.GetComponent<Money>;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject go = GameObject.Find("Main Camera");
            Money moneyCounter = go.GetComponent<Money>();
            moneyCounter.cash++;
            Destroy(gameObject);
        }
    }

}
