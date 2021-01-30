using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOffset : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    private void OnValidate()
    {
        UpdateOffset(horizontalSpeed, verticalSpeed);
    }

    private void Update()
    {
        UpdateOffset(horizontalSpeed, verticalSpeed);
    }

    private void UpdateOffset(float horizontalSpeed, float verticalSpeed)
    {
        var offset = new Vector2(horizontalSpeed, verticalSpeed);

        material.mainTextureOffset = offset;
    }
}
