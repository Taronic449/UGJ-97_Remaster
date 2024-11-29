using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    public AudioSource source;
    public void Play()
    {
        source.PlayOneShot(source.clip); 
    }
}
