public static class GridInfoSaver
{
    public static string SaveGridInfoToText(GridInfo _gridInfo)
    {
        string file = "";
        file += _gridInfo.BuildingInfos.Length + "\n";
        for(int i = 0; i < _gridInfo.BuildingInfos.Length; i++)
        {
            file += _gridInfo.BuildingInfos[i].X + "\n";
            file += _gridInfo.BuildingInfos[i].Y + "\n";
            file += _gridInfo.BuildingInfos[i].XSize + "\n";
            file += _gridInfo.BuildingInfos[i].YSize + "\n";
            file += _gridInfo.BuildingInfos[i].Type + "\n";
        }

        return file;
    }

    public static GridInfo LoadGridInfoToText(string file)
    {
        GridInfo gridInfo = new GridInfo();
        string[] lines = file.Split('\n');
        gridInfo.BuildingInfos = new BuildingInfo[int.Parse(lines[0])];
        for(int i = 0; i < gridInfo.BuildingInfos.Length; i++)
        {
            gridInfo.BuildingInfos[i] = new BuildingInfo();
            gridInfo.BuildingInfos[i].X = int.Parse(lines[i * 5 + 1]);
            gridInfo.BuildingInfos[i].Y = int.Parse(lines[i * 5 + 2]);
            gridInfo.BuildingInfos[i].XSize = int.Parse(lines[i * 5 + 3]);
            gridInfo.BuildingInfos[i].YSize = int.Parse(lines[i * 5 + 4]);
            gridInfo.BuildingInfos[i].Type = lines[i * 5 + 5].Trim();
        }
        return gridInfo;
    }
}