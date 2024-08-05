using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGradientBackground2D : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // Speed of the gradient movement
    private Renderer spriteRenderer;
    private Vector2 savedOffset;

    void Start()
    {
        spriteRenderer = GetComponent<Renderer>();
        savedOffset = spriteRenderer.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, savedOffset.y);
        spriteRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnDisable()
    {
        spriteRenderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}
