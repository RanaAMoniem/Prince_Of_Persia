using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer master;
   public void setSfx(float sfxlvl)
    {
        master.SetFloat("sfxVol", sfxlvl);
    }

    public void setmusic(float musiclvl)
    {
        master.SetFloat("musicVol", musiclvl);
    }

    public void setspeech(float speechlvl)
    {
        master.SetFloat("speechVol", speechlvl);
    }
}
