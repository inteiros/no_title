using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class PixelateEffect : MonoBehaviour
{
    public Material pixelateMaterial;
    public float pixelationFactor = 0.01f;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        pixelateMaterial.SetFloat("_PixelationFactor", pixelationFactor);
        Graphics.Blit(src, dest, pixelateMaterial);
    }
}
