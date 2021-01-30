using UnityEngine;

public class CharacterColor : MonoBehaviour
{
    [SerializeField] private Color color = Color.white;

    private void OnValidate()
    {
        var sprites = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = color;
        }
    }
}