using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerToSafeArea : MonoBehaviour
{
    private CanvasScaler canvasScaler;

    private void Awake()
    {
        canvasScaler = GetComponent<CanvasScaler>();
    }

    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        canvasScaler.referenceResolution = new Vector2(Screen.safeArea.width, Screen.safeArea.height);
#endif
    }
}