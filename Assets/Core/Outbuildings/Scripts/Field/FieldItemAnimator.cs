using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItemAnimator : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] private Color _ungrown;
    [SerializeField] private Color _grown;

    [Header("Scale")]
    [SerializeField] private float _ungrownScale;
    [SerializeField] private float _grownScale;

    [SerializeField] private Renderer _renderer;

    public void Animate(float state)
    {
        ChangeColor(state);
        ChangeScale(state);
    }

    private void ChangeColor(float state)
    {
        var currentColor = Color.Lerp(_ungrown, _grown, state);
        _renderer.material.color = currentColor;
    }

    private void ChangeScale(float state)
    {
        var currentScale = Mathf.Lerp(_ungrownScale, _grownScale, state);
        transform.localScale = Vector3.one * currentScale;
    }
}
