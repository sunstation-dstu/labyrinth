using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPCount : MonoBehaviour
{
    [Range(0,100)]
    public byte hp = 100;
    public Slider hpCount;
    public byte deathDuration;

    public byte hp1 = 100;
    public byte hp2;
    public bool death;
    public float Delay1 = 0;
    public float Delay2 = 0;

    public AudioSource HeroDamageSound;
    public AudioSource HeroDeathSound;
    public AudioSource FightMusic;

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
            if (death == true) death = false;
            hpCount.value = hp;
            hp2 = hp1;
            hp1 = hp;
            if (hp1 < hp2)
            {
                if (!FightMusic.isPlaying) FightMusic.Play();
                HeroDamageSound.Play();
                Delay1 = 20;
            }
            if (Delay1 > 0) Delay1 -= Time.deltaTime;
            else FightMusic.Stop();

            if ((Input.GetKey(KeyCode.X)) && (Delay2 <= 0)) { hp -= 5; Delay2 = 1; } // ударить себя (лол)
            if (Delay2 > 0) Delay2 -= Time.deltaTime;
        }
        else
        {
            hpCount.value = hp;
            hp = 0;
            t += Time.deltaTime / deathDuration;
            material.dissolveAmount = Mathf.Lerp(0, 1, t);

            if (death == false)
            {
                death = true;
                HeroDeathSound.Play();
            }
        }
    }
}
