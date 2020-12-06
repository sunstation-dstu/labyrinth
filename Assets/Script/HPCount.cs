using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (hp == 0)
        {
            SceneManager.LoadScene(0);  
        }
    }
}
