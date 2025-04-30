using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource ambianceBirdsAndLava;
    [SerializeField] AudioSource ambianceCicadasAndFire;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip birdslava;
    public AudioClip cicadasfire;
    public AudioClip playerdie;
    public AudioClip playerhurt;
    public AudioClip run;
    public AudioClip jump;
    public AudioClip lightattack;
    public AudioClip heavyattack;
    public AudioClip pickup;
    public AudioClip rathurt;
    public AudioClip bearhurt;
    public AudioClip rolleypolleyhurt;
    public AudioClip bouncemellow;

    private void Start()
    {
        musicSource.clip = background;
        ambianceBirdsAndLava.clip = birdslava;
        ambianceCicadasAndFire.clip = cicadasfire;
        musicSource.Play();
        ambianceBirdsAndLava.Play();
        ambianceCicadasAndFire.Play();
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
