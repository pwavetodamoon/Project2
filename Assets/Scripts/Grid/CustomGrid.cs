using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class CustomGrid
{
    private int width = 0;
    private int height = 0;
    private float cellSize = 0;
    private Vector3 originPosition = Vector3.zero;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;
    public bool showDebug = true;
    public CustomGrid(int width, int height, float cellSize, Vector3 originalPosition, bool showDebug = true)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originalPosition;
        this.gridArray = new int[width, height];
        this.showDebug = showDebug;
        if (showDebug)
            debugTextArray = new TextMesh[width, height];
        float duration = 10f;
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                if (showDebug)
                    debugTextArray[x, y] = Help.CreateTextMeshWorld(gridArray[x, y].ToString(), GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 150, null);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, duration);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, duration);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, duration);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, duration);
    }

    struct monsterSpawnCell
    {
        public monsterSpawnCell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x;
        public int y;
    }

    private monsterSpawnCell[] monstersSpawnArray;
    public void CreateSpawnArrayAtEnd(int x,int y)
    {
        Debug.Log($"CreateSpawnArrayAtEnd {x} {y}");
        int xMax = gridArray.GetLength(0) - 1;
        int xMin = xMax - x;
        int yMax = gridArray.GetLength(1) - 1;
        int yMin = yMax - y;
        int index = 0;
        monstersSpawnArray = new monsterSpawnCell[x * y];
        for (int i = xMax; i > xMin; i--)
        {
            for (int j = yMax; j > yMin; j--)
            {
                SetValue(i,j, 2);
                monstersSpawnArray[index] = new monsterSpawnCell(i, j);
                index++;
            }
        }
    }
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
        if(showDebug)
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        Debug.Log($"Grid x:{x} y:{y} value:{value} ");
    }
    public void Clear()
    {
        gridArray = new int[width, height];
    }
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }
    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
    public Vector3 GetWorldPosition(int x, int y)
    {
        //Debug.Log($"GetWorldPosition {new Vector3(x, y, 0)}");
        //Debug.Log($"GetWorldPosition {new Vector3(x, y, 0) * cellSize + originPosition}");
        return new Vector3(x, y, 0) * cellSize + originPosition;
    }
    public Vector3 GetCenterGridWorldPosition(int x, int y)
    {
        return GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f;
    }
}
