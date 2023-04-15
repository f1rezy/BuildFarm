using System;
using System.Collections; 
using UnityEngine;

public class Miner : MonoBehaviour
{
    [SerializeField] private Transform _toolPoint;
    
    private MiningTool _tool;
    private float _miningSpeed = 3f;
    
    private CharacterAnimator _animator;
    private IStorager _storager;

    private bool _isMining = false;
    public Action<int> OnMined;

    public bool ToolIsSet => _tool != null;
    
    public void SetTool(MiningTool tool)
    {
        if(ToolIsSet) Destroy(_tool.gameObject);
        _tool = Instantiate(tool, _toolPoint);
        _miningSpeed = _tool.MiningSpeed;
    }

    private void Start()
    {
        _animator = GetComponent<CharacterAnimator>();
        _storager = GetComponent<IStorager>();
    }

    private void OnEnable()
    {
        OnMined += Storage;
    }

    private void OnDisable()
    {
        OnMined -= Storage;
    }
    
    private void Update()
    {
        if (_isMining)
        {
            if (ToolIsSet)
                _animator.SetMining();
            else
                _animator.SetGathering();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FieldMineableItem mineable) &&
            _storager.CanStorage(mineable.PlantsPerMine) &&
            !_isMining)
        {
            if (mineable.CanMine())
            {
                OnMined += EnableMining;
                
                _isMining = true;

                var speed = ToolIsSet ? _tool.MiningSpeed : _miningSpeed;

                mineable.Mine(OnMined, speed);

                OnMined -= EnableMining;
            }
        }
    }

    private void EnableMining(int value) => _isMining = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FieldMineableItem mineable) &&
            _storager.CanStorage(mineable.PlantsPerMine))
        {
            _isMining = false;
            mineable.StopMining();
        }
    }

    private void Storage(int count ) => _storager.Add(count);
}
