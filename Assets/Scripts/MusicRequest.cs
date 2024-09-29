using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRequest : MonoBehaviour
{
    public AudioClip clip;
    public bool playOnStart;

    public void Play()
    {
        MusicManager.Instance.PlayMusic(clip);
    }

    void Start()
    {
        if(playOnStart)
        {
            MusicManager.Instance.PlayMusic(clip);
        }
    }
}
