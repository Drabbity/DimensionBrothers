using UnityEngine;

namespace DimensionBrothers.Dimension
{
    public class CameraScaleToLevel : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _levelSize;

        void Start()
        {
            float screenSizeRatio = Screen.width / (float)Screen.height;
            float levelSizeRatio = _levelSize.bounds.size.x / _levelSize.bounds.size.y;

            if (screenSizeRatio >= levelSizeRatio)
            {
                Camera.main.orthographicSize = _levelSize.bounds.size.y / 2;
            }
            else
            {
                float sizeDifference = levelSizeRatio / screenSizeRatio;
                Camera.main.orthographicSize = _levelSize.bounds.size.y / 2 * sizeDifference;
            }
        }
    }
}