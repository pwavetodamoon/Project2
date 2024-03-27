using Sirenix.OdinInspector;
using UnityEngine;

namespace SlotHero.Grid
{
    public class CustomGrid : MonoBehaviour
    {
        [SerializeField] private float cellSize;
        [SerializeField] private TextMesh[,] debugTextArray;
        [SerializeField] private int[,] gridArray;
        [SerializeField] private int height;
        [SerializeField] private int width;
        [SerializeField] Transform parent;
        [SerializeField] private Vector3 originPosition = Vector3.zero;
        public bool showDebug = true;
        [Button]
        private void ClearGrid()
        {
            if (debugTextArray.GetLength(0) > 0 && debugTextArray.GetLength(1) > 0)
            {
                for (var x = 0; x < debugTextArray.GetLength(0); x++)
                    for (var y = 0; y < debugTextArray.GetLength(1); y++)
                    {
                        Destroy(debugTextArray[x, y].gameObject);
                    }
            }
        }
        [Button]
        public void Init(bool showDebug = true)
        {
            // this.width = width;
            // this.height = height;
            // this.cellSize = cellSize;
            ClearGrid();
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
                            GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 150, parent);
                    // Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, duration);
                    // Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, duration);
                }

            // Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, duration);
            // Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, duration);
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
            //Debug.Log($"Grid x:{x} y:{y} value:{value} ");
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
        public Vector3 GetRandomPosition()
        {
            var x = Random.Range(0, width);
            var y = Random.Range(0, height);
            return GetCenterGridWorldPosition(x, y);
        }

        internal void Setup(int _width, int _height, int _cellSize, Vector3 _originalPos)
        {
            width = _width;
            height = _height;
            cellSize = _cellSize;
            originPosition = _originalPos;
        }
    }
}