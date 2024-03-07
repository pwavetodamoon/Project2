using Sirenix.OdinInspector;
using UnityEngine;

namespace Background
{
    /// <summary>
    ///     Controls the scrolling behavior of a background object.
    /// </summary>
    public class ScrollingBackground : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private float offset;
        [SerializeField] private bool isPaused = true;
        [SerializeField] private int sortingOrder;
        private Material material;
        private Renderer tr;

        private void Awake()
        {
            material = GetComponent<Renderer>().material;
        }

        private void Update()
        {
            ScrollHorizontal();
        }

        /// <summary>
        ///     Updates the sorting order of the background object.
        /// </summary>
        /// <param name="order">The new sorting order.</param>
        [Button]
        public void UpdateSortingOrder(int order)
        {
            tr = GetComponent<Renderer>();
            tr.sortingOrder = sortingOrder;
        }

        /// <summary>
        ///     Updates the current texture of the background object.
        /// </summary>
        /// <param name="texture">The new texture.</param>
        [Button]
        public void UpdateCurrentTexture(Texture2D texture)
        {
            if (texture != null) material.mainTexture = texture;
        }

        /// <summary>
        ///     Pauses the scrolling of the background.
        /// </summary>
        public void Pause()
        {
            isPaused = true;
        }

        /// <summary>
        ///     Resumes the scrolling of the background.
        /// </summary>
        public void Resume()
        {
            isPaused = false;
        }

        /// <summary>
        ///     Adjusts the scrolling Speed of the background.
        /// </summary>
        /// <param name="newSpeed">The new scrolling Speed.</param>
        public void AdjustSpeed(float newSpeed)
        {
            speed = newSpeed;
        }

        /// <summary>
        ///     Scrolls the background horizontally based on the current Speed.
        /// </summary>
        public void ScrollHorizontal()
        {
            if (isPaused) return;
            if (offset >= float.MaxValue) offset = 0;
            offset += Time.deltaTime * speed / 10f;
            material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
    }
}