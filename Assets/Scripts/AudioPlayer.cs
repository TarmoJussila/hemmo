using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip collectableAudioClip;
    [SerializeField] private AudioClip finishAudioClip;

    public void PlayCollectableAudioClip()
    {
        audioSource.PlayOneShot(collectableAudioClip);
    }

    public void PlayFinishAudioClip()
    {
        audioSource.PlayOneShot(finishAudioClip);
    }
}