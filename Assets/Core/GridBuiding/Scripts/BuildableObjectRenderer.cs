using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObjectRenderer : MonoBehaviour
{
    [SerializeField] private Color _canBuildColor = new Color(145, 255, 138, 141);
    [SerializeField] private Color _cantBuildColor = new Color(255, 143, 143, 141);
    [SerializeField] private Renderer[] _renderers;

    public void SetColorOnPositionChanged(bool canBuild)
    {
        foreach (var renderer in _renderers)
            renderer.material.color = canBuild ? _canBuildColor : _cantBuildColor;
    }
    
    public void SetDefault()
    {
        foreach (var renderer in _renderers)
            renderer.material.color = Color.white;
    }
}
