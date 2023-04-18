using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInfo
{
    private BuildingInfo[] _buildingInfos;

    public BuildingInfo[] BuildingInfos => _buildingInfos;

    public GridInfo(BuildableObjectGrid grid)
    {
        InitBuildingInfos(grid.Grid);
    }

    public GridInfo(BuildingInfo[] buildingInfos)
    {
        _buildingInfos = buildingInfos;
    }

    private void InitBuildingInfos(Dictionary<Vector2Int, BuildableObject> grid)
    {
        var buildings = grid.Values
            .Select(t => t)
            .Distinct()
            .ToArray();

        _buildingInfos = new BuildingInfo[buildings.Length];

        for (int i = 0; i < buildings.Length; i++)
        {
            var position = grid.Keys
                .Where(t => grid[t] == buildings[i])
                .OrderBy(t => t.x + t.y)
                .First();

            var type = string.Empty;

            if (buildings[i].TryGetComponent(out Field field)) type = "Field";
            if (buildings[i].TryGetComponent(out Hangar hangar)) type = "Hangar";
            if (buildings[i].TryGetComponent(out Market market)) type = "Market";

            _buildingInfos[i] = new BuildingInfo(position.x, position.y, buildings[i].Size.x, buildings[i].Size.y, type);
        }
    }
}
