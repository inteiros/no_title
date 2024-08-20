using UnityEngine;

[ExecuteInEditMode]
public class DitheringEffect : MonoBehaviour
{
    public Material ditheringMaterial;
    public float ditherThreshold = 0.5f;
    public float ditherStrength = 1.0f;
    public float ditherScale = 1.0f;
    public int patternIndex = 0;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (ditheringMaterial != null)
        {
            ditheringMaterial.SetFloat("_DitherThreshold", ditherThreshold);
            ditheringMaterial.SetFloat("_DitherStrength", ditherStrength);
            ditheringMaterial.SetFloat("_DitherScale", ditherScale);
            ditheringMaterial.SetInt("_PatternIndex", patternIndex);

            Graphics.Blit(src, dest, ditheringMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
