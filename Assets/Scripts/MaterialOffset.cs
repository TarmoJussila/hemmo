﻿using UnityEngine;

public class MaterialOffset : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private Transform followTransform;
    [SerializeField] private float followMultiplier = 0.05f;

#if UNITY_EDITOR
    private void OnApplicationQuit()
    {
        UpdateOffset(material, followTransform, 0f);
    }
#endif

    private void Update()
    {
        UpdateOffset(material, followTransform, followMultiplier);
    }

    private void UpdateOffset(Material material, Transform followTransform, float followMultiplier)
    {
        var offset = new Vector2(followTransform.position.x * followMultiplier, followTransform.position.y * followMultiplier);
        material.mainTextureOffset = offset;
    }
}