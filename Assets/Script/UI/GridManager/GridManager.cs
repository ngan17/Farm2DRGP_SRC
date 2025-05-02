using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int width = 100;
    public int height = 100;
    public float cellSize = 1f;

    private GridCell[,] grid;

    void Awake()
    {
        Instance = this;
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new GridCell[width, height];

        Vector3 origin = transform.position;  // Lấy vị trí GridManager làm gốc

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 worldPos = origin + new Vector3(x, y) * cellSize;  // Cộng thêm origin
                grid[x, y] = new GridCell(x, y, worldPos);
            }
        }
    }


    public Vector3 GetGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition.x - transform.position.x) / cellSize);
        int y = Mathf.FloorToInt((worldPosition.y - transform.position.y) / cellSize);

        // Kiểm tra nằm ngoài grid không
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            Debug.LogWarning($"GetGridPosition: ({x},{y}) nằm ngoài grid!");
            // Trả lại chính vị trí world nếu nằm ngoài phạm vi
            return new Vector3(
                Mathf.Floor(worldPosition.x / cellSize) * cellSize,
                Mathf.Floor(worldPosition.y / cellSize) * cellSize,
                0
            );
        }

        return grid[x, y].worldPosition;
    }


    public bool IsCellEmpty(int x, int y)
    {
        return !grid[x, y].isOccupied;
    }

    public void SetCellOccupied(int x, int y, bool occupied)
    {
        grid[x, y].isOccupied = occupied;
    }

    // Hiển thị grid trong editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Vector3 origin = transform.position;  // Lấy vị trí GridManager làm gốc

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 worldPos = origin + new Vector3(x, y) * cellSize;  // Cộng thêm origin
                Gizmos.DrawWireCube(worldPos, Vector3.one * cellSize);
            }
        }
    }

}

public class GridCell
{
    public int x, y;
    public Vector3 worldPosition;
    public bool isOccupied;

    public GridCell(int x, int y, Vector3 pos)
    {
        this.x = x;
        this.y = y;
        worldPosition = pos;
        isOccupied = false;
    }
}
