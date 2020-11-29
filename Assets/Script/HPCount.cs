using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPCount : MonoBehaviour
{
    [Range(0,100)]
    public int hp = 100;
    public Slider hpCount;
    public AudioSource HeartBite;

    void Update()
    {
        hpCount.value = hp;
        if (hp < 30)
        {
            HeartBite.Play();
        }
        else
        {
            HeartBite.Stop();
        }
    }
}
