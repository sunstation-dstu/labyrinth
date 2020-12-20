using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndySounds : MonoBehaviour
{
    /// <summary>
    /// Массив со звуковым сопровождением
    /// </summary>
    public AudioSource[] MusicData = new AudioSource[2];

    public float Delay = 300;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!MusicData[0].isPlaying) MusicData[0].Play();
        if (Delay <= 0)
        { MusicData[1].Play(); Delay = 300; }
        Delay -= Time.deltaTime;
    }
}
