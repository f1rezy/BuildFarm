using UnityEngine;

public class ToolItem: StoreItem
{
    [SerializeField] private Tool _tool;
    [SerializeField] private ToolManager _toolManager;
    protected override void Buy()
    {
        _toolManager.SetTool(_tool);
    }
}