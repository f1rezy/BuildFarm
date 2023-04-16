using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsSetuper : MonoBehaviour
{
    [SerializeField] private BuildableObjectGrid _grid;
    [SerializeField] private CameraFollower _camera;

    [SerializeField] private BuildableObject[] _objects;
    [SerializeField] private Transform[] _positions;

    private void Start()
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            _grid.CreateBuildingAndSet(_objects[i], _positions[i].position);
        }
    }
}
