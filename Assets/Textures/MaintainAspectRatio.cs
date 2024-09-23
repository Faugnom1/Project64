using UnityEngine;
using UnityEngine.UI;

public class MaintainAspectRatio : MonoBehaviour
{
    public int renderTextureWidth = 160;  // The width of your RenderTexture
    public int renderTextureHeight = 144; // The height of your RenderTexture

    private float targetAspect;
    private RectTransform rawImageRectTransform;

    void Start()
    {
        rawImageRectTransform = GetComponent<RectTransform>();

        // Calculate the target aspect ratio based on the render texture
        targetAspect = (float)renderTextureWidth / renderTextureHeight;

        // Adjust the size of the RawImage initially
        AdjustRawImageSize();
    }

    void Update()
    {
        // Adjust the size of the RawImage every frame (or you could optimize it for window resize only)
        AdjustRawImageSize();
    }

    void AdjustRawImageSize()
    {
        // Get the current screen's aspect ratio
        float screenAspect = (float)Screen.width / Screen.height;

        // Determine whether to letterbox or pillarbox the RawImage
        if (screenAspect >= targetAspect)
        {
            // If the screen is wider than the target aspect, scale based on height
            float newWidth = rawImageRectTransform.rect.height * targetAspect;
            rawImageRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        }
        else
        {
            // If the screen is taller than the target aspect, scale based on width
            float newHeight = rawImageRectTransform.rect.width / targetAspect;
            rawImageRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
        }
    }
}
