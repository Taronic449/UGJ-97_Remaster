using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    public static MusicManager Instance;

    public AudioClip target;
    public AudioClip curent;

    
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
        target = clip;
    }

    void FixedUpdate()
    {
        if(target != curent)
        {
            audioSource.volume -= 0.01f;

            if(audioSource.volume == 0)
            {
                curent = target;

                audioSource.clip = curent;
                audioSource.Play();
            }
        }
        else if(audioSource.volume < 1)
        {
            audioSource.volume += 0.01f;
        }
    }
}
