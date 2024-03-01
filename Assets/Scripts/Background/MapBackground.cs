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

        private ScrollingBackground[] ScrollingBackgroundArray;

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

        private bool CanLoadMap()
        {
            return MapIndex > 0 && MapIndex < mapBackgroundSO.MapCount();
        }

        [InfoBox("Load all texure for background array, use in play mode")]
        [Button]
        [DisableInEditorMode]
        public void LoadTexture()
        {
            if(CanLoadMap()== false) return;
            SpritesBackground textures2d = mapBackgroundSO.GetSpritesBackground(MapIndex);

            var textures = new[]
            {
                textures2d.plane, 
                textures2d.backWall, 
                textures2d.frontWall, 
                textures2d.groundDecor
            };

            for (int i = 0; i < ScrollingBackgroundArray.Length; i++)
            {
                ScrollingBackgroundArray[i].UpdateCurrentTexture(textures[i]);
                ScrollingBackgroundArray[i].UpdateSortingOrder(i);
            }
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

        public void GoNextMap()
        {
            MapIndex++;
            LoadTexture();
        }
    }
}