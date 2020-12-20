using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public GameObject terminalUI;
    
    private bool youIn;
    private connection tConnection;
    void Start()
    {
        tConnection = GetComponent<connection>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        youIn = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        youIn = false;
    }
    
    void Update()
    {
        if (youIn && Input.GetKeyDown(KeyCode.E) && tConnection.isActive)
            terminalUI.SetActive(!terminalUI.activeSelf);
        else if (!youIn || !tConnection.isActive)
            terminalUI.SetActive(false);
    }
}
