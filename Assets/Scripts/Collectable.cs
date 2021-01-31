using UnityEngine;

public class Collectable : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCollected && collision.CompareTag("Player"))
        {
            FindObjectOfType<ScoreKeeper>().AddScore();
            FindObjectOfType<AudioPlayer>().PlayCollectableAudioClip();
            isCollected = true;
            gameObject.SetActive(false);
        }
    }
}