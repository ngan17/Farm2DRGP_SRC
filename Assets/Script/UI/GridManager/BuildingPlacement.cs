using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    public GameObject buildingPrefab;  // Kéo prefab nhà vào đây

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Click trái chuột
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;  // Giữ Z = 0 (2D)

            // Tính toán vị trí grid
            int gridX = Mathf.FloorToInt((mouseWorldPos.x - GridManager.Instance.transform.position.x) / GridManager.Instance.cellSize);
            int gridY = Mathf.FloorToInt((mouseWorldPos.y - GridManager.Instance.transform.position.y) / GridManager.Instance.cellSize);

            // Kiểm tra nếu nằm trong grid
            if (gridX >= 0 && gridY >= 0 && gridX < GridManager.Instance.width && gridY < GridManager.Instance.height)
            {
                // Kiểm tra ô trống
                if (GridManager.Instance.IsCellEmpty(gridX, gridY))
                {
                    // Lấy vị trí đặt nhà snap vào grid
                    Vector3 placePos = GridManager.Instance.GetGridPosition(mouseWorldPos);
                    Instantiate(buildingPrefab, placePos, Quaternion.identity);

                    // Đánh dấu ô này là occupied
                    GridManager.Instance.SetCellOccupied(gridX, gridY, true);

                    Debug.Log($"Đã đặt nhà tại ({gridX}, {gridY})");
                }
                else
                {
                    Debug.Log("Ô này đã có nhà rồi!");
                }
            }
            else
            {
                Debug.Log("Ngoài phạm vi grid!");
            }
        }
    }
}
