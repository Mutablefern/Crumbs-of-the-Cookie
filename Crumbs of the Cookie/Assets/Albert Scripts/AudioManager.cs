using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] audioSources;
    [SerializeField] AudioClip[] audioClips;

    public void playAudio(int id)
    {
        audioSources[id].clip = audioClips[id];
    }
}