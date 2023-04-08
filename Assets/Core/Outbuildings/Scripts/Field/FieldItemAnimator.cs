using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItemAnimator : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] private Color _ungrown;
    [SerializeField] private Color _grown;
    [SerializeField] private Color _hitColor;

    [Header("Scale")]
    [SerializeField] private float _ungrownScale;
    [SerializeField] private float _grownScale;

    [SerializeField] private float _miningAminationDuration = 0.25f;

    [SerializeField] private Renderer _renderer;

    public IEnumerator AnimateMining(float duration)
    {
        var defaultColor = _renderer.material.color;

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            var value = Mathf.PingPong(t, _miningAminationDuration);
            _renderer.material.color = Color.Lerp(defaultColor, _hitColor, value);
            yield return null;
        }
        _renderer.material.color = defaultColor;
    }
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
