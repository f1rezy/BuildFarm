using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothing = 3f;
    [SerializeField] private float _charactetSize = 4f;
    [SerializeField] private float _buildingSize = 8f;

    private Transform _target;
    private float _targetSize;

    private void FixedUpdate()
    {
        Move();
    }

    private void Start()
    {
        _target = _character;
        _offset = transform.position - _target.position;
    }

    private void Move()
    {
        var nextPosition = Vector3.Lerp(transform.position, _target.position + _offset, Time.fixedDeltaTime * _smoothing);

        transform.position = nextPosition;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _targetSize = _buildingSize;
        StopAllCoroutines();
        StartCoroutine(SetCameraSize());
        _smoothing = 1f;
    }

    public void SetCharacterTarget()
    {
        _target = _character;
        _targetSize = _charactetSize;
        StopAllCoroutines();
        StartCoroutine(SetCameraSize());
        _smoothing = 3f;
    }

    private IEnumerator SetCameraSize()
    {
        while (Camera.main.orthographicSize != _targetSize)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, _targetSize, Time.deltaTime * 5);
            yield return null;
        }
    }
}
