using UnityEngine;

public class BuildingItem: StoreItem
{
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private GameObject _marketView;

    [SerializeField] private BuildableObject _buildingPrefab;
    [SerializeField] private BuildableObjectGrid _buildableObjectGrid;

    protected override void Buy()
    {
        _marketView.SetActive(false);

        Vector2 screenPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Physics.Raycast(ray, out RaycastHit hit);
        Vector3 position = hit.transform.position;
        var building = _buildableObjectGrid.CreateBuilding(_buildingPrefab, position);

        _cameraFollower.SetTarget(building.transform);
    }
}
