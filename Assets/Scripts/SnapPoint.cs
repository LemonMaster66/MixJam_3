using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    [Header("Properties")]
    public bool Empty;
    public string Part;
    public string Variant;
    public float Distance;

    [Range(0,1)] public float TargetAlpha = 0;
    [Range(0,1)] public float Blend = 0;

    private SpriteRenderer spriteRenderer;
    public Color colour;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Distance);
    }

    void Update()
    {
        colour.a = Mathf.SmoothDamp(spriteRenderer.color.a, TargetAlpha, ref Blend, 0.1f);
        spriteRenderer.color = colour;
    }
}