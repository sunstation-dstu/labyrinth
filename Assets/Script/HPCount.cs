using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPCount : MonoBehaviour
{
    [Range(0,100)]
    public int hp = 100;
    public Slider hpCount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpCount.value = hp;
    }
}
