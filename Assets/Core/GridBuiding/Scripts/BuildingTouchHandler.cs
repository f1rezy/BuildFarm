using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTouchHandler : MonoBehaviour
{
    public event Action OnEnter;
    public event Action<Vector3> OnDrag;
    public event Action OnExit;

    private Plane _groundPlane;
    private Camera _camera;

    private void Awake()
    {
        _groundPlane = new Plane(Vector3.up, Vector3.zero);
        _camera = Camera.main;
    }

    private void OnMouseEnter()
    {
        OnEnter?.Invoke();
    }

    private void OnMouseUp()
    {
        OnExit?.Invoke();
    }

    private void OnMouseDrag()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (_groundPlane.Raycast(ray, out float position))
        {
            Vector3 worldPosition = ray.GetPoint(position);
            OnDrag?.Invoke(worldPosition);
        }
    }
}
