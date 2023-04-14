using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObjectRenderer : MonoBehaviour
{
    [SerializeField] private Color _canBuildColor;
    [SerializeField] private Color _cantBuildColor;
    [SerializeField] private Renderer _renderer;

    public void SetColorOnPositionChanged(bool canBuild)
    {
        _renderer.material.color = canBuild ? _canBuildColor : _cantBuildColor;
    }
    
    public void SetDefault()
    {
        _renderer.material.color = Color.white;
    }
}
