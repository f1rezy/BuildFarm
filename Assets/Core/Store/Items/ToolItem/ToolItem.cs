using UnityEngine;

public class ToolItem: StoreItem
{
    [SerializeField] private MiningTool _tool;
    [SerializeField] private Miner _miner;
    protected override void Buy()
    {
        _miner.SetTool(_tool);
    }
}