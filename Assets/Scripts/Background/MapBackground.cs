using Sirenix.OdinInspector;
using UnityEngine;

// TODO:Background 7,8,9 have a issue when scrolling, it's not smooth
namespace Background
{
    public class MapBackground : MonoBehaviour
    {
        [SerializeField] private MapBackgroundSO mapBackgroundSO;

        [InfoBox("Map name use for testing, just on play mode, it just temporary")]
        public int MapIndex = 0;

        [ShowInInspector] private ScrollingBackground[] ScrollingBackgroundArray;

        private void Start()
        {
            LoadField();
            LoadTexture();
            StartScrolling();
        }

        /// <summary>
        /// Loads the field by getting the components of ScrollingBackground in the children objects.
        /// </summary>
        [Button]
        private void LoadField()
        {
            ScrollingBackgroundArray = GetComponentsInChildren<ScrollingBackground>();

        }


        [InfoBox("Load all texure for background array, use in play mode")]
        [Button]
        [DisableInEditorMode]
        public void LoadTexture()
        {
            // make sure array is not null or empty
            if (ScrollingBackgroundArray == null || ScrollingBackgroundArray.Length == 0)
            {
                LoadField();
            }

            SpritesBackground textures2d = mapBackgroundSO.GetSpritesBackground(MapIndex);

            Texture2D Plane = textures2d.plane;
            Texture2D BackWall = textures2d.backWall;
            Texture2D FrontWall = textures2d.frontWall;
            Texture2D GroundDecor = textures2d.groundDecor;

            ScrollingBackgroundArray[0].UpdateCurrentTexture(Plane);
            ScrollingBackgroundArray[0].UpdateSortingOrder(0);

            ScrollingBackgroundArray[1].UpdateCurrentTexture(BackWall);
            ScrollingBackgroundArray[1].UpdateSortingOrder(1);

            ScrollingBackgroundArray[2].UpdateCurrentTexture(FrontWall);
            ScrollingBackgroundArray[2].UpdateSortingOrder(2);

            ScrollingBackgroundArray[3].UpdateCurrentTexture(GroundDecor);
            ScrollingBackgroundArray[3].UpdateSortingOrder(3);

        }

        /// <summary>
        /// Adjusts the scrolling Speed of the background.
        /// </summary>
        /// <param name="speed">The Speed value to adjust the scrolling Speed.</param>
        [Button]
        public void AdjustSpeed(float speed = .5f)
        {
            foreach (var item in ScrollingBackgroundArray)
            {
                item.AdjustSpeed(speed);
            }
        }

        /// <summary>
        /// Starts scrolling the background.
        /// </summary>
        [Button]
        public void StartScrolling()
        {
            foreach (var item in ScrollingBackgroundArray)
            {
                item.Resume();
            }
        }

        /// <summary>
        /// Stops scrolling the background.
        /// </summary>
        [Button]
        public void StopScrolling()
        {
            foreach (var item in ScrollingBackgroundArray)
            {
                item.Pause();
            }
        }
    }
}