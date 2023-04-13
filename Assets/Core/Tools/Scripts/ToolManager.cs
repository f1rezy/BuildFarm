using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public class ToolManager: MonoBehaviour
{
    [SerializeField] private Transform _toolPoint;
    private Tool _tool;

    private IStorager _storager;
    private CharacterAnimator _animator;
    
    public bool IsMining { get; private set; }
    
    private IEnumerator Mine()
    {
        yield return new WaitForSeconds(1f);
        IsMining = false;
    }
    
    private void Start()
    {
        _animator = GetComponent<CharacterAnimator>();
        _storager = GetComponent<IStorager>();
    }

    private bool CanMine()
    {
        Ray ray = new Ray(transform.position + transform.up * 0.5f,  transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow);
        Physics.Raycast(ray, out RaycastHit hit);
        return hit.collider != null && hit.collider.gameObject.TryGetComponent(out FieldMineableItem mineable) &&
               hit.distance < _tool.MinDistanceToMine;
    }

    public bool ToolIsSet => _tool != null;

    public void SetTool(Tool tool)
    {
        if(ToolIsSet) Destroy(_tool.gameObject);
        _tool = Instantiate(tool, _toolPoint);
        _tool._storager = _storager;
    }

    private void Update()
    {
        if (CanMine() && !IsMining && _storager.CanStorage(3))
        {
            _animator.SetMining();
            StartCoroutine(Mine());
        }
    }
}
