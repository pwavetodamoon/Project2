using UnityEngine;

namespace Helper
{
    public static class GameLayerMask
    {
        public static string ITEMS = "Items";
        public static string HERO = "Hero";
        public static string ENEMY = "Enemy";

        public static LayerMask Get(string layer)
        {
            return 1 << LayerMask.NameToLayer(layer);
        }
    }
}