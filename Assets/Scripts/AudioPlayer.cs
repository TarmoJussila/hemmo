using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip collectableAudioClip;

    public void PlayCollectableAudioClip()
    {
        audioSource.PlayOneShot(collectableAudioClip);
    }
}