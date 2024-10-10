using UnityEngine;

namespace Lagrange
{
    public static class LayerUtility
    {
        public static int Default => LayerMask.NameToLayer("Default");
        public static int UI => LayerMask.NameToLayer("UI");

        public static int LayerToLayerMask(int layer)
        {
            return 1 << layer;
        }

        public static bool IsGameObjectOnLayer(GameObject gObj, int layer)
        {
            return LayerCompare(gObj.layer, layer);
        }

        public static bool LayerCompare(int layer1, int layer2)
        {
            return layer1 == layer2;
        }
    }
}
