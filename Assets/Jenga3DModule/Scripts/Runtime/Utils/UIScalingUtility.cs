using UnityEngine;
using UnityEngine.UI;

namespace Jenga3DModule.Scripts.Runtime.Utils
{
    public static class UIScalingUtility
    {
        public static Vector2 GetCanvasScale(CanvasScaler canvasScaler)
        {
            var width = Mathf.Min(Screen.currentResolution.width, Screen.width);
            var height = Mathf.Min(Screen.currentResolution.height, Screen.height);
            var referenceWidth = canvasScaler.referenceResolution.x;
            var referenceHeight = canvasScaler.referenceResolution.y;

            Vector2 canvasScale = new Vector2((width / referenceWidth), (height / referenceHeight));
            return canvasScale;
        }
    }
}