using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offcet;
    [SerializeField] private float _smoothing = 3f;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var nextPosition = Vector3.Lerp(transform.position, _target.position + _offcet, Time.fixedDeltaTime * _smoothing);

        transform.position = nextPosition;
    }
}
