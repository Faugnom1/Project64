using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DarknessController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private MaterialPropertyBlock _materialPropertyBlock;

    public Bounds DarknessBounds { get {  return _spriteRenderer.bounds; } }

    // TODO: Can pre hash these
    private string _positiveCutoffID;
    private string _negativeCutoffID;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();

        _positiveCutoffID = "_PositiveCutoff";
        _negativeCutoffID = "_NegativeCutoff";
    }

    public void SetPositiveCutoff(Vector2 cutoff)
    {
        _spriteRenderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetVector(_positiveCutoffID, cutoff);
        _spriteRenderer.SetPropertyBlock(_materialPropertyBlock);
    }

    public void SetNegativeCutoff(Vector2 cutoff)
    {
        _spriteRenderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetVector(_negativeCutoffID, cutoff);
        _spriteRenderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
