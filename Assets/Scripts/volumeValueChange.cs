using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeValueChange : MonoBehaviour
{
    private AudioSource audiosrc;
    private float musicVolume = 1f;
    void Start()
    {
        audiosrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audiosrc.volume = musicVolume;
    }
    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
