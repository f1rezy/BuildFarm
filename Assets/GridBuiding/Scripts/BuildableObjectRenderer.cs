using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObjectRenderer : MonoBehaviour
{
    [SerializeField] private Color _canBuildColor = Color.green;
    [SerializeField] private Color _cantBuildColor = Color.red;
    [SerializeField] private Renderer _renderer;

    public void OnCanBuild()
    {
        _renderer.material.color = _canBuildColor;
    }

    public void OnCantBuild()
    {
        _renderer.material.color = _cantBuildColor;
    }

    public void SetDefault()
    {
        _renderer.material.color = Color.white;
    }
}
