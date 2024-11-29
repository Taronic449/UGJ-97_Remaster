using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioSource fsxAudioSource;

    public static MusicManager Instance;
    public AudioClip target;
    public AudioClip curent;

    [Header("Clips")]
    public AudioClip press;

    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        fsxAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
        
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
    public void PressSound()
    {
        fsxAudioSource.PlayOneShot(press);
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
