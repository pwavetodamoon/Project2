using UnityEngine;

namespace SlotHero.Grid
{
    public class CustomGrid
    {
        private readonly float cellSize;
        private readonly TextMesh[,] debugTextArray;
        private int[,] gridArray;
        private readonly int height;

        private monsterSpawnCell[] monstersSpawnArray;
        private readonly Vector3 originPosition = Vector3.zero;
        public bool showDebug = true;
        private readonly int width;

        public CustomGrid(int width, int height, float cellSize, Vector3 originalPosition, bool showDebug = true)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            originPosition = originalPosition;
            gridArray = new int[width, height];
            this.showDebug = showDebug;
            if (showDebug)
                debugTextArray = new TextMesh[width, height];
            var duration = 10f;
            for (var x = 0; x < gridArray.GetLength(0); x++)
            for (var y = 0; y < gridArray.GetLength(1); y++)
            {
                if (showDebug)
                    debugTextArray[x, y] = Help.CreateTextMeshWorld(gridArray[x, y].ToString(),
                        GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 150, null);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, duration);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, duration);
            }

            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, duration);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, duration);
        }

        public void CreateSpawnArrayAtEnd(int x, int y)
        {
            Debug.Log($"CreateSpawnArrayAtEnd {x} {y}");
            var xMax = gridArray.GetLength(0) - 1;
            var xMin = xMax - x;
            var yMax = gridArray.GetLength(1) - 1;
            var yMin = yMax - y;
            var index = 0;
            monstersSpawnArray = new monsterSpawnCell[x * y];
            for (var i = xMax; i > xMin; i--)
            for (var j = yMax; j > yMin; j--)
            {
                SetValue(i, j, 2);
                monstersSpawnArray[index] = new monsterSpawnCell(i, j);
                index++;
            }
        }

        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        }

        public void SetValue(int x, int y, int value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height) gridArray[x, y] = value;
            if (showDebug)
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
                return gridArray[x, y];
            return 0;
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

        private struct monsterSpawnCell
        {
            public monsterSpawnCell(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public int x;
            public int y;
        }
    }
}