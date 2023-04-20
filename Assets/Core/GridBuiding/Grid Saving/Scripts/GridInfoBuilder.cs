using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInfoBuilder
{
    private GridInfo _gridInfo;

    public GridInfo GridInfo => _gridInfo;

    public BuildingInfo[] BuildingInfos => _gridInfo.BuildingInfos;

    public GridInfoBuilder(BuildableObjectGrid grid)
    {
        InitBuildingInfos(grid.Grid);
    }

    public GridInfoBuilder(BuildingInfo[] buildingInfos)
    {
        _gridInfo = new GridInfo();
        _gridInfo.BuildingInfos = buildingInfos;
    }

    private void InitBuildingInfos(Dictionary<Vector2Int, BuildableObject> grid)
    {
        var buildings = grid.Values
            .Select(t => t)
            .Distinct()
            .ToArray();
        _gridInfo = new GridInfo();
        _gridInfo.BuildingInfos = new BuildingInfo[buildings.Length];

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

            _gridInfo.BuildingInfos[i] = new BuildingInfo()
            {
                X = position.x,
                Y = position.y,
                XSize = buildings[i].Size.x,
                YSize = buildings[i].Size.y,
                Type = type
            };
        }
    }
}
