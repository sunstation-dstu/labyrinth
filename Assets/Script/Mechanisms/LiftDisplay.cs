using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDisplay : MonoBehaviour
{
    public GameObject display;
    public Sprite spriteOff;
    public Sprite spriteUp;
    public Sprite spriteDown;
    public GameObject lightUp;
    public GameObject lightDown;

    private elevator elevator;
    private SpriteRenderer sRenderer;
    void Start()
    {
        elevator = GetComponent<elevator>();
        sRenderer = display.GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if (!elevator.isActive)
        {
            sRenderer.sprite = spriteOff;
            lightDown.SetActive(false);
            lightUp.SetActive(false);
        }
        else if (elevator.isActive && elevator.direction == elevator.upperSum)
        {
            sRenderer.sprite = spriteUp;
            lightDown.SetActive(false);
            lightUp.SetActive(true);
        }
        else if (elevator.isActive && elevator.direction == elevator.upperStay)
        {
            sRenderer.sprite = spriteDown;
            lightDown.SetActive(true);
            lightUp.SetActive(false);
        }
    }
}
