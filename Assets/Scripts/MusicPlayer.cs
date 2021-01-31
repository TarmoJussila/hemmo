using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip endMusic;
    [SerializeField] private float volumeFadeSpeed = 0.1f;

    private bool hasStartedCoroutine = false;
    private float defaultVolume;

    private void Start()
    {
        defaultVolume = audioSource.volume;
    }

    public void PlayEndMusic()
    {
        if (hasStartedCoroutine)
        {
            return;
        }

        StartCoroutine(PlayEndMusicCoroutine());
        hasStartedCoroutine = true;
    }

    private IEnumerator PlayEndMusicCoroutine()
    {
        float volume = audioSource.volume;
        while (volume > 0f)
        {
            volume -= Time.deltaTime * volumeFadeSpeed;
            volume = Mathf.Clamp(volume, 0f, defaultVolume);
            audioSource.volume = volume;
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
        yield return null;

        audioSource.clip = endMusic;
        audioSource.Play();
        yield return null;

        while (volume < defaultVolume)
        {
            volume += Time.deltaTime * volumeFadeSpeed;
            volume = Mathf.Clamp(volume, 0f, defaultVolume);
            audioSource.volume = volume;
            yield return null;
        }
    }
}