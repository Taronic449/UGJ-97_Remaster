using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    private float fadeDuration = 3f;
    private bool isFading;
    public static MusicManager Instance;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void PlayMusic(AudioClip clip)
    {
        audioSource.clip = clip;

        if(audioSource.isPlaying)
        {
            FadeOutAndPlayNext(clip);
        }
        else
        {
            audioSource.volume = 0;
            audioSource.clip = clip;
            audioSource.Play();
            StartCoroutine(FadeInCoroutine());
        }
        
    }

    public void FadeOutAndPlayNext(AudioClip clip)
    {
        if(!isFading)
            StartCoroutine(FadeOutCoroutine(clip));
    }

    IEnumerator FadeOutCoroutine(AudioClip clip)
    {
        isFading = true;
        float startVolume = audioSource.volume;

        // Gradually decrease the volume over fadeDuration seconds
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = 0f;
        audioSource.Stop();

        isFading = false;
        
        StartCoroutine(FadeInCoroutine());
        

    }

    IEnumerator FadeInCoroutine()
    {
        float startVolume = 0f;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 1f, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 1f;
    }

}
