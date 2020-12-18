using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPCount : MonoBehaviour
{
    [Range(0,100)]
    public int hp = 100;
    public Slider hpCount;
    public int deathDuration;
    
    private DissolvedMaterial material;
    private float t;
    
    void Start()
    {
        material = GetComponent<DissolvedMaterial>();
    }
    
    private void Death()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp > 0)
        {
            hpCount.value = hp;
        }
        else
        {  
            t += Time.deltaTime / deathDuration;
            material.dissolveAmount = Mathf.Lerp(0, 1, t);
        }
    }
}
