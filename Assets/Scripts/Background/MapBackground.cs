using Sirenix.OdinInspector;
using UnityEngine;

// TODO:Background 7,8,9 have a issue when scrolling, it's not smooth
namespace Background
{
    public class MapBackground : MonoBehaviour
    {
        [SerializeField] private MapBackgroundSO mapBackgroundSO;

        [InfoBox("Map name use for testing, just on play mode, it just temporary")]
        public int MapIndex => stageInformation == null ? 0 : stageInformation.currentMapIndex;
        public StageInformation stageInformation;
        private ScrollingBackground[] ScrollingBackgroundArray;
        private void Start()
        {
            LoadField();
            LoadTexture();
            StartScrolling();
        }

        private void OnValidate()
        {
            if (stageInformation == null)
            {
                stageInformation = GetDataSupport.Get().StageInformation;
            }
        }

        [Button]
        private void LoadField()
        {
            ScrollingBackgroundArray = GetComponentsInChildren<ScrollingBackground>();
        }

        private bool CanLoadMap() => MapIndex > 0 && MapIndex < mapBackgroundSO.MapCount();

        [InfoBox("Load all texure for background array, use in play mode")]
        [Button]
        [DisableInEditorMode]
        public void LoadTexture()
        {
            if (CanLoadMap() == false) return;
            var textures2d = mapBackgroundSO.GetSpritesBackground(0);

            var textures = new[]
            {
                textures2d.plane,
                textures2d.backWall,
                textures2d.frontWall,
                textures2d.groundDecor
            };

            for (var i = 0; i < ScrollingBackgroundArray.Length; i++)
            {
                ScrollingBackgroundArray[i].UpdateCurrentTexture(textures[i]);
                ScrollingBackgroundArray[i].UpdateSortingOrder(i);
            }
        }

        [Button]
        public void AdjustSpeed(float speed = .5f)
        {
            foreach (var item in ScrollingBackgroundArray) item.AdjustSpeed(speed);
        }


        [Button]
        public void StartScrolling()
        {
            foreach (var item in ScrollingBackgroundArray) item.Resume();
        }

        [Button]
        public void StopScrolling()
        {
            foreach (var item in ScrollingBackgroundArray) item.Pause();
        }

        public void GoNextMap()
        {
            if (MapIndex < 0 || MapIndex >= mapBackgroundSO.MapCount()) return;
            LoadTexture();
        }
    }
}