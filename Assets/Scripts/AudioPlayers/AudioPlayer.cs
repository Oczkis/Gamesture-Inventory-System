using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private int lastAudioPlayedIndex;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomAudioSound()
    {
        int randomClipIndex = Random.Range(0, audioClips.Length);

        if (randomClipIndex == lastAudioPlayedIndex)
            randomClipIndex++;


        audioSource.clip = audioClips[randomClipIndex % audioClips.Length];
        audioSource.Play();

        lastAudioPlayedIndex = randomClipIndex;
    }
}
