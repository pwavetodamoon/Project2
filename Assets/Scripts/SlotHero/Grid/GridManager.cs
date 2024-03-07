using Helper;
using UnityEngine;

namespace SlotHero.Grid
{
    public class GridManager : Singleton<GridManager>
    {
        // Start is called before the first frame update
        public CustomGrid Combat_Grid;

        protected override void Awake()
        {
            base.Awake();
            Combat_Grid = new CustomGrid(22, 7, 1, new Vector3(-9, -5), false);
            Combat_Grid.CreateSpawnArrayAtEnd(3, 7);
        }

        public CustomGrid GetGrid()
        {
            return Combat_Grid;
        }
    }

    public static class Help
    {
        public static TextMesh CreateTextMeshWorld(string text, Vector2 localPosition, int fontSize, Transform parent)
        {
            var Scale = new Vector3(0.03f, 0.03f, 0.03f);

            var gameObject = new GameObject("World Text", typeof(TextMesh));
            var transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            transform.localScale = Scale;
            var textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = Color.white;
            return textMesh;
        }
    }
}