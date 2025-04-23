using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource Ambiens;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip birds;
    public AudioClip death;
    public AudioClip run;
    public AudioClip movingplatform;
    public AudioClip pickup;
    public AudioClip jump;
    public AudioClip playerhurt;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        Ambiens.clip = birds;
        Ambiens.Play();
    }

    public void playSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    //Copy this into the script where it's going to play the sound

    //Cach this: AudioManager audioManager;

    //private void Awake()
    //{
    //    audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    //}

    //Where the action and sound is going to be: audioManager.playSFX(audioManager.nameOfTheSound);
}
