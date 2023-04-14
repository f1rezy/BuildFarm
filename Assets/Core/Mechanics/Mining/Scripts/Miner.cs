using System;
using System.Collections; 
using UnityEngine;

public class Miner : MonoBehaviour
{
    [SerializeField] private Transform _toolPoint;
    
    private MiningTool _tool;
    private float _miningSpeed = 1f;
    
    private CharacterAnimator _animator;
    private IStorager _storager;

    private bool _isMining = false;
    public Action<int> OnMined;

    public bool ToolIsSet => _tool != null;
    public bool IsMiningAnimation { get; private set; }
    
    private IEnumerator Mine()
    {
        yield return new WaitForSeconds(1f);
        IsMiningAnimation = false;
    }

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
        if (!IsMiningAnimation && _isMining)
        {
            if (ToolIsSet)
                _animator.SetMining();
            else
                _animator.SetGathering();
            StartCoroutine(Mine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FieldMineableItem mineable) &&
            _storager.CanStorage(mineable.PlantsPerMine) &&
            !_isMining)
        {
            if (mineable.CanMine())
            {
                OnMined += EnableMining;
                
                _isMining = true;
                mineable.Mine(OnMined, _miningSpeed);

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
            mineable.StopMining();
            _isMining = false;
        }
    }

    private void Storage(int count ) => _storager.Add(count);
}
